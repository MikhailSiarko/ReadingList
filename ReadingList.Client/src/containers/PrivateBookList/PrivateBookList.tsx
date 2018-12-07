import * as React from 'react';
import { RootState } from '../../store/reducers';
import { PrivateBookListItem, SelectListItem, PrivateBookList as PrivateList, Book } from '../../models';
import PrivateBookLI from '../../components/PrivateBookLI';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService } from '../../services';
import { withContextMenu, closeContextMenues, withSpinner } from '../../hoc';
import BookList from '../../components/BookList';
import PrivateListNameEditor from '../../components/PrivateListNameEditForm';
import { createPropAction } from '../../utils';
import { loadingActions } from '../../store/actions/loading';
import { RouteComponentProps } from 'react-router';
import Search from '../../components/Search';
import { BookService } from '../../services/BookService';
import BookSearchItem from '../../components/BookSearchItem';

interface Props extends RouteComponentProps<any> {
    loading: boolean;
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
}

interface State {
    isFormHidden: boolean;
}

class PrivateBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {isFormHidden: true};
    }

    async componentDidMount() {
        if (!this.props.bookList || !this.props.statuses) {
            this.props.loadingStart();
            if (!this.props.bookList) {
                await this.props.getList();
            }
            if (!this.props.statuses) {
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

    handleSearchItemClick = async (item: Book) => {
        this.props.loadingStart();
        await this.props.addItem(item.id);
        this.props.loadingEnd();
    }

    handleUpdateList = async (newName: string) => {
        this.props.loadingStart();
        await this.props.updateList({name: newName} as PrivateList);
        this.props.loadingEnd();
    }

    renderSearchItem = (item: Book) => <BookSearchItem book={item} />;

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
            />
        );
    }

    renderLegend = () => (
        this.props.bookList.isInEditMode ?
            (
                <PrivateListNameEditor
                    name={this.props.bookList.name}
                    onSave={this.handleUpdateList}
                    onCancel={this.props.switchListEditMode}
                />
            ) : this.props.bookList.name
    )

    renderPrivateBookListPage = () => {
        let listItems;

        if (this.props.bookList.items.length > 0) {
            listItems = this.props.bookList.items.map(this.mapItem);
        }

        const bookListActions = [{onClick: this.props.switchListEditMode, text: 'Edit list name'}];
        const ContexedList = withContextMenu(bookListActions, BookList);

        return (
            <>
                <Search
                    onSubmit={this.props.findBooks}
                    itemRender={this.renderSearchItem}
                    onItemClick={this.handleSearchItemClick}
                />
                <ContexedList items={listItems} legend={this.renderLegend()} />
            </>
        );
    }

    render() {
        const Spinnered = withSpinner(!this.props.loading && this.isDataLoaded(), this.renderPrivateBookListPage);
        return <Spinnered />;
    }

    private isDataLoaded() {
        return this.props.bookList != null && this.props.statuses != null;
    }
}

function mapStateToProps(state: RootState) {
    return {
        bookList: state.private.list,
        statuses: state.private.bookStatuses,
        loading: state.loading
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