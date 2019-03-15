import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import BookList from '../../components/BookList';
import SharedBookLI from '../../components/SharedBookLI';
import {
    Book,
    User,
    Chunked,
    Tag,
    SharedBookListItem,
    SelectListItem,
    ListInfo,
    SharedBookList as SharedList,
    SharedListUpdateData
} from '../../models';
import { connect, Dispatch } from 'react-redux';
import { withContextMenu, closeContextMenues } from '../../hoc';
import SharedListEditForm from '../../components/SharedListEditForm';
import AddBookForm from '../../components/AddBookForm/AddBookForm';
import FixedGroup from '../../components/FixedGroup';
import RoundButton from '../../components/RoundButton';
import SharedListLegend from '../../components/SharedListLegend';
import SharedBookForm from '../../components/SharedBookForm';
import { RootState, bookActions, moderatedListActions } from 'src/store';
import { sharedListActions } from 'src/store/sharedList/actions';

interface Props extends RouteComponentProps<any> {
    list: SharedList;
    books: Chunked<Book>;
    moderatedLists: ListInfo[];
    moderators: User[];
    tags: Tag[];
    fetchList: (id: number) => void;
    addItem: (listId: number, bookId: number) => void;
    deleteItem: (listId: number, itemId: number) => void;
    updateList: (id: number, data: SharedListUpdateData) => void;
    findBooks: (query: string, chunk: number) => void;
    fetchModeratedLists: () => void;
    shareBook: (itemId: number, lists: number[]) => void;
    deleteList: (listId: number) => void;
    switchToEditMode: () => void;
    switchToSimpleMode: () => void;
    clearSharedListState: () => void;
    clearBookState: () => void;
    clearModeratedListsState: () => void;
}

interface State {
    tagOptions: SelectListItem[] | null;
    isFormHidden: boolean;
    bookSearchQuery: string | null;
    sharingBookId: number | null;
    shareBookFormHidden: boolean;
}

class SharedBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            tagOptions: null,
            bookSearchQuery: null,
            isFormHidden: true,
            shareBookFormHidden: true,
            sharingBookId: null
        };
    }

    componentDidMount() {
        const id = parseInt(this.props.match.params.id, 10);
        this.props.fetchList(id);
    }

    componentWillUnmount() {
        this.props.clearSharedListState();
        this.props.clearBookState();
        this.props.clearModeratedListsState();
    }

    handleSaveList = (name: string, tags: Tag[], moderators: number[]) => {
        this.props.updateList(
            this.props.list.id,
            {
                name,
                moderators,
                tags
            }
        );
    }

    mapModerator = (user: User): SelectListItem => ({ text: user.login, value: user.id });

    mapTag = (tag: Tag): SelectListItem => ({ text: tag.name, value: tag.id });

    renderLegend = () => {
        if (this.props.list) {
            if(this.props.list.isInEditMode) {
                return (
                    <SharedListEditForm
                        tags={this.props.list.tags}
                        moderators={this.props.list.moderators}
                        moderatorsOptions={
                            this.props.moderators
                                ? this.props.moderators.map(this.mapModerator)
                                : null
                        }
                        name={this.props.list.name}
                        tagsOptions={
                            this.props.tags
                                ? this.props.tags.map(this.mapTag)
                                : null
                        }
                        onSave={this.handleSaveList}
                        onCancel={this.props.switchToSimpleMode}
                    />
                );
            }
            return (
                <SharedListLegend
                    moderators={this.props.list.moderators}
                    name={this.props.list.name}
                    tags={this.props.list.tags}
                />
            );
        } else {
            return '';
        }
    }

    deleteItem = (item: SharedBookListItem) => {
        closeContextMenues();
        const confirmDeleting = confirm(
            `Do you really want to delete the item "${item.title}" by ${item.author}`);

        if (confirmDeleting) {
            if(this.props.list) {
                this.props.deleteItem(this.props.list.id, item.id);
            }
        } else {
            return;
        }
    }

    mapItem = (item: SharedBookListItem) => {
        const actions = [
            {
                onClick: () => this.showShareBookForm(item.bookId),
                text: 'Share'
            }
        ];
        if(this.props.list && this.props.list.canBeModerated) {
            actions.push(
                {
                    text: 'Delete',
                    onClick: () => this.deleteItem(item)
                },
            );
        }
        const Contexed = withContextMenu(actions, SharedBookLI);
        return <Contexed key={item.id} item={item} onDelete={() => this.deleteItem(item)} />;
    }

    closeForm = (_: React.MouseEvent<HTMLButtonElement>) => {
        this.setState({
            isFormHidden: true,
            bookSearchQuery: null
        });
        this.props.clearBookState();
    }

    showBooksForm = () => {
        this.props.findBooks('', 1);
        this.setState({
            isFormHidden: false
        });
    }

    handleSearchChange = (query: string) => {
        this.props.findBooks(query, 1);
    }

    handleAddBook = (id: number) => {
        this.props.addItem(this.props.list.id, id);
        this.setState({
            isFormHidden: true
        });
    }

    showShareBookForm = (bookId: number) => {
        this.props.fetchModeratedLists();
        this.setState({
            shareBookFormHidden: false,
            sharingBookId: bookId
        });
    }

    handleShareBook = (bookId: number, listsIds: number[]) => {
        this.props.shareBook(bookId, listsIds);
        this.setState({
            shareBookFormHidden: true,
            sharingBookId: null
        });
    }

    handleCancelSharingBook = () => {
        this.setState({
            shareBookFormHidden: true,
            sharingBookId: null
        });
        this.props.clearModeratedListsState();
    }

    handlePaging = (newChunkNumber: number) => {
        const query = this.state.bookSearchQuery as string;
        this.props.findBooks(query, newChunkNumber);
    }

    handleNext = () => {
        const chunk = this.props.books.chunkInfo.chunk + 1;
        this.handlePaging(chunk);
    }

    handlePrevious = () => {
        const chunk = this.props.books.chunkInfo.chunk - 1;
        this.handlePaging(chunk);
    }

    removeList = () => {
        const confirmDeleting = confirm(
            `Do you really want to delete the item this list?`);

        if (confirmDeleting) {
            this.props.deleteList(this.props.list.id);
        } else {
            return;
        }
    }

    render() {
        let actions = [];

        if(this.props.list && this.props.list.editable) {
            actions.push({
                text: 'Edit',
                onClick: () => this.props.switchToEditMode()
            });
        }

        let Contexed = withContextMenu(actions, BookList);

        if (this.props.list) {
            return (
                <>
                    <Contexed items={this.props.list.items.map(this.mapItem)} legend={this.renderLegend()} />
                    {
                        this.props.list.canBeModerated && (
                            <>
                                <FixedGroup>
                                    <RoundButton
                                        radius={3}
                                        title="Add book"
                                        onClick={this.showBooksForm}
                                    >
                                        <i className="fas fa-book" />
                                    </RoundButton>
                                    {
                                        this.props.list.editable
                                            ? (
                                                <RoundButton
                                                    radius={3}
                                                    title="Delete"
                                                    onClick={this.removeList}
                                                >
                                                    <i className="fas fa-trash" />
                                                </RoundButton>
                                            )
                                            : null
                                    }
                                </FixedGroup>
                                {
                                    !this.state.isFormHidden && this.props.books &&
                                        <AddBookForm
                                            hasNext={this.props.books.chunkInfo.hasNext}
                                            hasPrevious={this.props.books.chunkInfo.hasPrevious}
                                            onNext={this.handleNext}
                                            onPrevious={this.handlePrevious}
                                            books={this.props.books.items ? this.props.books.items : []}
                                            searchQuery={this.state.bookSearchQuery ? this.state.bookSearchQuery : ''}
                                            onSubmit={this.handleAddBook}
                                            onCancel={this.closeForm}
                                            onQueryChange={this.handleSearchChange}
                                        />
                                }
                            </>
                        )
                    }
                    {
                        !this.state.shareBookFormHidden &&
                            <SharedBookForm
                                options={this.props.moderatedLists ? this.props.moderatedLists : []}
                                onSubmit={this.handleShareBook}
                                onCancel={this.handleCancelSharingBook}
                                choosenBookId={this.state.sharingBookId}
                            />
                    }
                </>
            );
        }
        return null;
    }
}

function mapStateToProps(state: RootState) {
    return {
        list: state.shared.list,
        books: state.books,
        moderatedLists: state.moderatedLists,
        moderators: state.shared.moderators,
        tags: state.tags
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        fetchList: (id: number) => {
            dispatch(sharedListActions.fetchListBegin(id));
        },
        updateList: (id: number, data: SharedListUpdateData) => {
            dispatch(sharedListActions.updateListBegin(id, data));
        },
        addItem: (listId: number, bookId: number) => {
            dispatch(sharedListActions.addItemBegin(listId, bookId));
        },
        deleteItem: (listId: number, itemId: number) => {
            dispatch(sharedListActions.deleteItemBegin(listId, itemId));
        },
        findBooks: (query: string, chunk: number | null) => {
            dispatch(bookActions.findBegin(query, chunk));
        },
        shareBook: (bookId: number, lists: number[]) => {
            dispatch(bookActions.shareBook(bookId, lists));
        },
        fetchModeratedLists: () => {
            dispatch(moderatedListActions.fetchBegin());
        },
        deleteList: (listId: number) => {
            dispatch(sharedListActions.deleteListBegin(listId));
        },
        switchToEditMode: () => {
            dispatch(sharedListActions.switchListEditModeBegin());
        },
        switchToSimpleMode: () => {
            dispatch(sharedListActions.switchListSimpleMode());
        },
        clearSharedListState: () => {
            dispatch(sharedListActions.clearShared());
        },
        clearBookState: () => {
            dispatch(bookActions.clearBookState());
        },
        clearModeratedListsState: () => {
            dispatch(moderatedListActions.clearModeratedListsState());
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SharedBookList);