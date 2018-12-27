import * as React from 'react';
import { RootState } from '../../store/reducers';
import { PrivateBookListItem, SelectListItem, PrivateBookList as PrivateList, Book } from '../../models';
import PrivateBookLI from '../../components/PrivateBookLI';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService } from '../../services';
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
    loadingStart: () => void;
    loadingEnd: () => void;
    findBooks: (query: string) => Promise<Book[]>;
    shareList: (name: string) => Promise<void>;
}

interface State {
    bookFormHidden: boolean;
    shareFormHidden: boolean;
    books: Book[] | null;
    bookSearchQuery: string | null;
}

class PrivateBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            bookFormHidden: true,
            books: null,
            bookSearchQuery: null,
            shareFormHidden: true
        };
    }

    async componentDidMount() {
        if (!this.props.bookList || !this.props.statuses) {
            this.props.loadingStart();
            if(!this.props.bookList) {
                await this.props.getList();
            }
            if(!this.props.statuses) {
                await this.props.getBookStatuses();
            }
            this.props.loadingEnd();
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
        this.props.loadingStart();
        await this.props.updateList({name: newName} as PrivateList);
        this.props.loadingEnd();
    }

    handleUpdateItem = async (item: PrivateBookListItem) => {
        this.props.loadingStart();
        await this.props.updateItem(item);
        this.props.loadingEnd();
    }

    mapItem = (item: PrivateBookListItem) => {
        const actions = [
            {onClick: () => this.props.switchItemEditMode(item.id), text: 'Edit'},
            {
                onClick: this.deleteItem(
                    item,
                    async itemId => {
                        this.props.loadingStart();
                        await this.props.deleteItem(itemId);
                        this.props.loadingEnd();
                    }
                ),
                text: 'Delete'
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
                onEditButtonClick={this.props.switchItemEditMode}
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
                ? (
                    <h3 style={{fontWeight: 400, margin: 0}}>
                        {this.props.bookList.name.toUpperCase()}
                    </h3>
                )
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
        this.props.loadingStart();
        const books = await this.props.findBooks('');
        if(books) {
            this.setState({
                books,
                bookFormHidden: false
            }, () => this.props.loadingEnd());
        } else {
            this.setState({
                bookFormHidden: false
            }, () => this.props.loadingEnd());
        }
    }

    showShareForm = () => {
        this.setState({
            shareFormHidden: false
        });
    }

    handleSearchChange = async (query: string) => {
        this.props.loadingStart();
        const books = await this.props.findBooks(query);
        if(books) {
            this.setState({
                books,
                bookSearchQuery: query
            }, () => this.props.loadingEnd());
        } else {
            this.props.loadingEnd();
        }
    }

    handleAddBook = async (id: number) => {
        this.props.loadingStart();
        await this.props.addItem(id);
        this.setState({
            books: null,
            bookFormHidden: true
        }, () => this.props.loadingEnd());
    }

    handleShareCancel = () => {
        this.setState({
            shareFormHidden: true
        });
    }

    handleShareFormSubmit = async (name: string) => {
        this.props.loadingStart();
        await this.props.shareList(name);
        this.setState({
            shareFormHidden: true
        }, () => this.props.loadingEnd());
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
                    <RoundButton radius={3} title="Share this list" onClick={this.showShareForm}>⛬</RoundButton>
                    <RoundButton radius={3} title="Add book" onClick={this.showBooksForm}>+</RoundButton>
                </FixedGroup>
                <AddBookForm
                    hidden={this.state.bookFormHidden}
                    books={this.state.books ? this.state.books : []}
                    searchQuery={this.state.bookSearchQuery ? this.state.bookSearchQuery : ''}
                    onSubmit={this.handleAddBook}
                    onCancel={this.closeForm}
                    onQueryChange={this.handleSearchChange}
                />
                <ShareForm
                    hidden={this.state.shareFormHidden}
                    onCancel={this.handleShareCancel}
                    onSubmit={this.handleShareFormSubmit}
                />
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
        shareList: async (name: string) => {
            const result = await listService.sharePrivateList(name);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
        },
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        },
        findBooks: async (query: string) => {
            const result = await bookService.findBooks(query);
            if (!result.isSucceed) {
                alert(result.errorMessage);
            }
            return result.data;
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);