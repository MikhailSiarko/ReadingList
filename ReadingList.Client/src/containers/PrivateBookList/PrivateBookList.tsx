import * as React from 'react';
import {
    PrivateBookListItem,
    SelectListItem,
    PrivateBookList as PrivateList,
    Book,
    ListInfo,
    Chunked,
    PrivateItemUpdateData,
    PrivateListUpdateData
} from '../../models';
import {
    PrivateBookItem,
    BookList,
    PrivateListEditor,
    AddBookForm,
    ShareForm,
    PrivateListLegend,
    ShareBookForm
} from '../../components';
import { connect, Dispatch } from 'react-redux';
import { withContextMenu } from '../../hoc';
import { RouteComponentProps } from 'react-router';
import { privateListActions, RootState, bookActions, moderatedListActions } from '../../store';
import PrivateListActions from 'src/components/PrivateListActions/PrivateListActions';

interface Props extends RouteComponentProps<any> {
    bookList: PrivateList;
    statuses: SelectListItem[];
    books: Chunked<Book>;
    moderatedLists: ListInfo[];
    addItem: (bookId: number) => void;
    updateList: (data: PrivateListUpdateData) => void;
    deleteItem: (itemId: number) => void;
    updateItem: (itemId: number, data: PrivateItemUpdateData) => void;
    switchItemEditMode: (itemId: number) => void;
    switchListEditMode: () => void;
    fetchList: () => void;
    findBooks: (query: string, chunk: number | null) => Chunked<Book>;
    shareList: (name: string) => void;
    getModeratedLists: () => ListInfo[];
    shareBook: (itemId: number, lists: number[]) => void;
    clearBookState: () => void;
    clearModeratedListsState: () => void;
}

interface State {
    bookFormHidden: boolean;
    shareFormHidden: boolean;
    shareBookFormHidden: boolean;
    bookSearchQuery: string | null;
    sharingBookId: number | null;
}

class PrivateBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            bookFormHidden: true,
            bookSearchQuery: null,
            shareFormHidden: true,
            shareBookFormHidden: true,
            sharingBookId: null
        };
    }

    componentDidMount() {
        if(!this.props.bookList) {
            this.props.fetchList();
        }
    }

    deleteItem(item: PrivateBookListItem, deleteFromProps: (itemId: number) => void) {
        return function () {
            const confirmDeleting = confirm(
                `Do you really want to delete the item "${item.title}" by ${item.author}`);

            if (confirmDeleting) {
                deleteFromProps(item.id);
            } else {
                return;
            }
        };
    }

    handleUpdateList = (data: PrivateListUpdateData) => {
        this.props.updateList(data);
    }

    handleUpdateItem = (itemId: number, data: PrivateItemUpdateData) => {
        this.props.updateItem(itemId, data);
    }

    mapItem = (item: PrivateBookListItem) => {
        const deleteItem = this.deleteItem(
            item,
            itemId => {
                this.props.deleteItem(itemId);
            }
        );

        const actions = [
            {
                onClick: () => this.props.switchItemEditMode(item.id),
                text: 'Edit'},
            {
                onClick: deleteItem,
                text: 'Delete'
            },
            {
                onClick: () => this.showShareBookForm(item.bookId),
                text: 'Share'
            }
        ];

        const Contexed = withContextMenu(actions, PrivateBookItem);

        return (
            <Contexed
                key={item.id}
                listItem={item}
                onSave={this.handleUpdateItem}
                onCancel={this.props.switchItemEditMode}
                statuses={this.props.statuses}
                onEdit={this.props.switchItemEditMode}
                onDelete={deleteItem}
            />
        );
    }

    renderLegend = () => (
        this.props.bookList && this.props.bookList.isInEditMode ?
            (
                <PrivateListEditor
                    name={this.props.bookList.name}
                    onSave={this.handleUpdateList}
                    onCancel={this.props.switchListEditMode}
                />
            ) : this.props.bookList
                ? <PrivateListLegend name={this.props.bookList.name} />
                : null
    )

    closeForm = (_: React.MouseEvent<HTMLButtonElement>) => {
        this.setState({
            bookFormHidden: true,
            bookSearchQuery: null
        });
        this.props.clearBookState();
    }

    showBooksForm = () => {
        this.props.findBooks('', 1);
        this.setState({
            bookFormHidden: false
        });
    }

    showShareForm = () => {
        this.setState({
            shareFormHidden: false
        });
    }

    handleSearchChange = (query: string) => {
        this.props.findBooks(query, 1);
        this.setState({
            bookSearchQuery: query
        });
    }

    handleAddBook = (id: number) => {
        this.props.addItem(id);
        this.setState({
            bookFormHidden: true
        });
    }

    handleShareCancel = () => {
        this.setState({
            shareFormHidden: true
        });
        this.props.clearModeratedListsState();
    }

    showShareBookForm = (bookId: number) => {
        this.props.getModeratedLists();
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

    handleShareFormSubmit = (name: string) => {
        this.props.shareList(name);
        this.setState({
            shareFormHidden: true
        });
    }

    handleCancelSharingBook = () => {
        this.setState({
            shareBookFormHidden: true,
            sharingBookId: null
        });
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

    render() {
        let listItems;

        if (this.props.bookList && this.props.bookList.items && this.props.bookList.items.length > 0) {
            listItems = this.props.bookList.items.map(this.mapItem);
        }

        const bookListActions = [
            {
                onClick: this.props.switchListEditMode,
                text: 'Edit list name'
            }
        ];

        const ContexedList = withContextMenu(bookListActions, BookList);

        return (
            <>
                <ContexedList items={listItems} legend={this.renderLegend()} />
                <PrivateListActions onShare={this.showShareForm} onAddBook={this.showBooksForm} />
                {
                    !this.state.bookFormHidden && this.props.books &&
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
                {
                    !this.state.shareFormHidden &&
                        <ShareForm
                            onCancel={this.handleShareCancel}
                            onSubmit={this.handleShareFormSubmit}
                        />
                }
                {
                    !this.state.shareBookFormHidden && this.props.moderatedLists &&
                        <ShareBookForm
                            options={this.props.moderatedLists}
                            onSubmit={this.handleShareBook}
                            onCancel={this.handleCancelSharingBook}
                            chosenBookId={this.state.sharingBookId}
                        />
                }
            </>
        );
    }
}

function mapStateToProps(state: RootState) {
    return {
        bookList: state.private.list,
        statuses: state.private.bookStatuses,
        books: state.books,
        moderatedLists: state.moderatedLists
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        addItem: (bookId: number) => {
            dispatch(privateListActions.addItemBegin(bookId));
        },
        deleteItem: (itemId: number) => {
            dispatch(privateListActions.deleteItemBegin(itemId));
        },
        updateItem: (itemId: number, data: PrivateItemUpdateData) => {
            dispatch(privateListActions.updateItemBegin(itemId, data));
        },
        switchItemEditMode: (itemId: number) => {
            dispatch(privateListActions.switchItemEditMode(itemId));
        },
        fetchList: () => {
            dispatch(privateListActions.fetchListBegin());
        },
        switchListEditMode: () => {
            dispatch(privateListActions.switchListEditMode());
        },
        updateList: (data: PrivateListUpdateData) => {
            dispatch(privateListActions.updateListBegin(data));
        },
        getModeratedLists: () => {
            dispatch(moderatedListActions.fetchBegin());
        },
        shareList: (name: string) => {
            dispatch(privateListActions.shareList(name));
        },
        findBooks: (query: string, chunk: number | null) => {
            dispatch(bookActions.findBegin(query, chunk));
        },
        shareBook: (bookId: number, lists: number[]) => {
            dispatch(bookActions.shareBook(bookId, lists));
        },
        clearBookState: () => {
            dispatch(bookActions.clearBookState());
        },
        clearModeratedListsState: () => {
            dispatch(moderatedListActions.clearModeratedListsState());
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);