import * as React from 'react';
import { RootState } from '../../store/reducers';
import { PrivateBookListItem, SelectListItem, PrivateBookList as PrivateList } from '../../models';
import PrivateBookLI from '../../components/PrivateBookLI';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService } from '../../services';
import { withContextMenu, closeContextMenues } from '../../hoc';
import BookList from '../../components/BookList';
import ItemForm from '../../components/ItemForm';
import PrivateListNameEditor from '../../components/PrivateListNameEditForm';
import { createPropAction } from '../../utils';
import { loadingActions } from '../../store/actions/loading';
import { RouteComponentProps } from 'react-router';
import Spinner from '../../components/Spinner';

interface Props extends RouteComponentProps<any> {
    loading: boolean;
    bookList: PrivateList;
    statuses: SelectListItem[];
    addItem: (listItem: PrivateBookListItem) => Promise<void>;
    updateList: (list: PrivateList) => Promise<void>;
    deleteItem: (itemId: number) => Promise<void>;
    updateItem: (item: PrivateBookListItem) => Promise<void>;
    switchItemEditMode: (itemId: number) => void;
    switchListEditMode: () => void;
    getList: () => Promise<void>;
    getBookStatuses: () => Promise<void>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

function deleteItem(item: PrivateBookListItem, deleteFromProps: (itemId: number) => Promise<void>) {
    return async function() {
        closeContextMenues();
        const confirmDeleting = confirm(
            `Do you really want to delete the item "${item.title}" by ${item.author}`);

        if(confirmDeleting) {
            await deleteFromProps(item.id);
        } else {
            return;
        }
    };
}

class PrivateBookList extends React.Component<Props> {
    async componentDidMount() {
        if(!this.props.bookList || !this.props.statuses) {
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

    render() {
        if(!this.props.loading && this.isDataLoaded()) {
            let list;
            let listItems;
            if(this.props.bookList.items.length > 0) {
                listItems = this.props.bookList.items.map(listItem => {
                    const actions = [
                        {onClick: () => this.props.switchItemEditMode(listItem.id), text: 'Edit'},
                        {onClick: deleteItem(listItem, async itemId => {
                                this.props.loadingStart();
                                await this.props.deleteItem(itemId);
                                this.props.loadingEnd();
                            }), text: 'Delete'}
                    ];
                    const Contexed = withContextMenu(actions, PrivateBookLI);

                    return (
                        <Contexed
                            key={listItem.id}
                            listItem={listItem}
                            onSave={async item => {
                                this.props.loadingStart();
                                await this.props.updateItem(item);
                                this.props.loadingEnd();
                            }}
                            onCancel={this.props.switchItemEditMode}
                            statuses={this.props.statuses}
                        />
                    );
                });
            }
            const legend = (
                this.props.bookList.isInEditMode ?
                (
                    <PrivateListNameEditor
                        name={this.props.bookList.name}
                        onSave={async newName => {
                            this.props.loadingStart();
                            await this.props.updateList({name: newName} as PrivateList);
                            this.props.loadingEnd();
                        }}
                        onCancel={this.props.switchListEditMode}
                    />
                ) : this.props.bookList.name
            );
            const bookListActions = [{onClick: this.props.switchListEditMode, text: 'Edit list name'}];
            const ContexedList = withContextMenu(bookListActions, BookList);
            list = <ContexedList items={listItems} legend={legend} />;

            return (
                <div>
                    {
                        this.props.bookList && !this.props.bookList.isInEditMode
                            ? <ItemForm onSubmit={async item => {
                                this.props.loadingStart();
                                await this.props.addItem(item);
                                this.props.loadingEnd();
                            }} /> : null
                    }
                    {list}
                </div>
            );
        }

        return <Spinner />;
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
    const bookService = new PrivateBookListService();
    return {
        addItem: createPropAction(bookService.addItem, dispatch, privateBookListAction.addItem),
        deleteItem: createPropAction(bookService.removeItem, dispatch, privateBookListAction.removeItem),
        updateItem: createPropAction(bookService.updateItem, dispatch, privateBookListAction.updateItem),
        switchItemEditMode: (itemId: number) => {
            dispatch(privateBookListAction.switchEditModeForItem(itemId));
        },
        getList: createPropAction(bookService.getList, dispatch, privateBookListAction.updateList),
        switchListEditMode: () => {
            dispatch(privateBookListAction.switchEditModeForList());
        },
        updateList: createPropAction(bookService.updateList, dispatch, privateBookListAction.updateList),
        getBookStatuses: createPropAction(bookService.getBookStatuses, dispatch,
            privateBookListAction.setBookStatuses),
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);