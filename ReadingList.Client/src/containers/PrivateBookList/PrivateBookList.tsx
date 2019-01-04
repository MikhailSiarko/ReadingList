import * as React from 'react';
import { RootState } from '../../store/reducers';
import { PrivateBookListItem, SelectListItem, PrivateBookList as PrivateList, Book, ListInfo } from '../../models';
import PrivateBookLI from '../../components/PrivateBookLI';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService, ListsService } from '../../services';
import { withContextMenu, closeContextMenues } from '../../hoc';
import BookList from '../../components/BookList';
import PrivateListEditor from '../../components/PrivateListEditForm';
import { createPropAction, processFailedRequest } from '../../utils';
import { loadingActions } from '../../store/actions/loading';
import { RouteComponentProps } from 'react-router';
import { BookService } from '../../services/BookService';
import AddBookForm from '../../components/AddBookForm';
import RoundButton from '../../components/RoundButton';
import FixedGroup from '../../components/FixedGroup';
import ShareForm from '../../components/ShareForm';
import PrivateListLegend from '../../components/PrivateListLegend';
import ShareBookForm from '../../components/ShareBookForm';

interface Props extends RouteComponentProps<any> {
    bookList: PrivateList;
    statuses: SelectListItem[];
    addItem: (bookId: number) => Promise<void>;
    updateList: (list: PrivateList) => Promise<void>;
    deleteItem: (itemId: number) => Promise<void>;
    updateItem: (item: PrivateBookListItem) => Promise<void>;
    switchItemEditMode: (itemId: number) => void;
    switchListEditMode: () => void;
    getList: () => Promise<void>;
    getBookStatuses: () => Promise<void>;
    startLoading: () => void;
    endLoading: () => void;
    findBooks: (query: string) => Promise<Book[]>;
    shareList: (name: string) => Promise<void>;
    getModeratedLists: () => Promise<ListInfo[]>;
    shareBook: (itemId: number, lists: number[]) => Promise<void>;
}

interface State {
    bookFormHidden: boolean;
    shareFormHidden: boolean;
    shareBookFormHidden: boolean;
    moderatedLists: ListInfo[] | null;
    books: Book[] | null;
    bookSearchQuery: string | null;
    sharingBookId: number | null;
}

class PrivateBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            bookFormHidden: true,
            books: null,
            bookSearchQuery: null,
            shareFormHidden: true,
            shareBookFormHidden: true,
            moderatedLists: null,
            sharingBookId: null
        };
    }

    async componentDidMount() {
        if (!this.props.bookList || !this.props.statuses) {
            this.props.startLoading();
            if(!this.props.bookList) {
                await this.props.getList();
            }
            if(!this.props.statuses) {
                await this.props.getBookStatuses();
            }
            this.props.endLoading();
        }
    }

    shouldComponentUpdate(nextProps: Props) {
        return nextProps.bookList != null && nextProps.statuses != null;
    }

    deleteItem(item: PrivateBookListItem, deleteFromProps: (itemId: number) => Promise<void>) {
        return async function () {
            closeContextMenues();
            const confirmDeleting = confirm(
                `Do you really want to delete the item "${item.title}" by ${item.author}`);

            if (confirmDeleting) {
                await deleteFromProps(item.id);
            } else {
                return;
            }
        };
    }

    handleUpdateList = async (newName: string) => {
        this.props.startLoading();
        await this.props.updateList({name: newName} as PrivateList);
        this.props.endLoading();
    }

    handleUpdateItem = async (item: PrivateBookListItem) => {
        this.props.startLoading();
        await this.props.updateItem(item);
        this.props.endLoading();
    }

    mapItem = (item: PrivateBookListItem) => {
        const deleteItem = this.deleteItem(
            item,
            async itemId => {
                this.props.startLoading();
                await this.props.deleteItem(itemId);
                this.props.endLoading();
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

        const Contexed = withContextMenu(actions, PrivateBookLI);

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

    closeForm = (event: React.MouseEvent<HTMLButtonElement>) => {
        this.setState({
            bookFormHidden: true,
            books: null,
            bookSearchQuery: null
        });
    }

    showBooksForm = async () => {
        this.props.startLoading();
        const books = await this.props.findBooks('');
        if(books) {
            this.setState({
                books,
                bookFormHidden: false
            }, () => this.props.endLoading());
        } else {
            this.setState({
                bookFormHidden: false
            }, () => this.props.endLoading());
        }
    }

    showShareForm = () => {
        this.setState({
            shareFormHidden: false
        });
    }

    handleSearchChange = async (query: string) => {
        this.props.startLoading();
        const books = await this.props.findBooks(query);
        if(books) {
            this.setState({
                books,
                bookSearchQuery: query
            }, () => this.props.endLoading());
        } else {
            this.props.endLoading();
        }
    }

    handleAddBook = async (id: number) => {
        this.props.startLoading();
        await this.props.addItem(id);
        this.setState({
            books: null,
            bookFormHidden: true
        }, () => this.props.endLoading());
    }

    handleShareCancel = () => {
        this.setState({
            shareFormHidden: true
        });
    }

    showShareBookForm = async (bookId: number) => {
        this.props.startLoading();
        const moderatedLists = await this.props.getModeratedLists();
        if(moderatedLists) {
            this.setState({
                shareBookFormHidden: false,
                moderatedLists,
                sharingBookId: bookId
            }, () => this.props.endLoading());
        } else {
            this.setState({
                shareBookFormHidden: false,
                sharingBookId: bookId
            }, () => this.props.endLoading());
        }
    }

    handleShareBook = async (bookId: number, listsIds: number[]) => {
        this.props.startLoading();
        await this.props.shareBook(bookId, listsIds);
        this.setState({
            moderatedLists: null,
            shareBookFormHidden: true,
            sharingBookId: null
        }, () => this.props.endLoading());
    }

    handleShareFormSubmit = async (name: string) => {
        this.props.startLoading();
        await this.props.shareList(name);
        this.setState({
            shareFormHidden: true
        }, () => this.props.endLoading());
    }

    handleCancelSharingBook = () => {
        this.setState({
            shareBookFormHidden: true,
            sharingBookId: null
        });
    }

    render() {
        let listItems;

        if (this.props.bookList && this.props.bookList.items.length > 0) {
            listItems = this.props.bookList.items.map(this.mapItem);
        }

        const bookListActions = [{onClick: this.props.switchListEditMode, text: 'Edit list name'}];
        const ContexedList = withContextMenu(bookListActions, BookList);

        return (
            <>
                <ContexedList items={listItems} legend={this.renderLegend()} />
                <FixedGroup>
                    <RoundButton radius={3} title="Share this list" onClick={this.showShareForm}>
                        <i className="fas fa-share-alt" />
                    </RoundButton>
                    <RoundButton radius={3} title="Add book" onClick={this.showBooksForm}>
                        <i className="fas fa-book" />
                    </RoundButton>
                </FixedGroup>
                {
                    !this.state.bookFormHidden &&
                        <AddBookForm
                            books={this.state.books ? this.state.books : []}
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
                    !this.state.shareBookFormHidden &&
                        <ShareBookForm
                            options={this.state.moderatedLists ? this.state.moderatedLists : []}
                            onSubmit={this.handleShareBook}
                            onCancel={this.handleCancelSharingBook}
                            choosenBookId={this.state.sharingBookId}
                        />
                }
            </>
        );
    }
}

function mapStateToProps(state: RootState) {
    return {
        bookList: state.private.list,
        statuses: state.private.bookStatuses
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const listService = new PrivateBookListService();
    const bookService = new BookService();
    return {
        addItem: createPropAction(listService.addItem, dispatch, privateBookListAction.addItem),
        deleteItem: createPropAction(listService.removeItem, dispatch, privateBookListAction.removeItem),
        updateItem: createPropAction(listService.updateItem, dispatch, privateBookListAction.updateItem),
        switchItemEditMode: (itemId: number) => {
            dispatch(privateBookListAction.switchEditModeForItem(itemId));
        },
        getList: createPropAction(listService.getList, dispatch, privateBookListAction.updateList),
        switchListEditMode: () => {
            dispatch(privateBookListAction.switchEditModeForList());
        },
        updateList: createPropAction(listService.updateList, dispatch, privateBookListAction.updateList),
        getBookStatuses: createPropAction(listService.getBookStatuses, dispatch,
            privateBookListAction.setBookStatuses),
        getModeratedLists: async () => {
            const result = await new ListsService().getModeratedLists();
            if(!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        shareList: async (name: string) => {
            const result = await listService.sharePrivateList(name);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
        },
        startLoading: () => {
            dispatch(loadingActions.start());
        },
        endLoading: () => {
            dispatch(loadingActions.end());
        },
        findBooks: async (query: string) => {
            const result = await bookService.findBooks(query);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        shareBook: async (bookId: number, lists: number[]) => {
            const result = await bookService.shareBook(bookId, lists);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);