import * as React from 'react';
import { RootState } from '../../store/reducers';
import { PrivateBookListItem, RequestResult, SelectListItem, PrivateBookListModel } from '../../models';
import PrivateBookLI from '../../components/PrivateBookLI';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService } from '../../services';
import { withContextMenu, closeContextMenues } from '../../hoc/withContextMenu';
import PrivateBookUL from '../../components/PrivateBookUL';
import ItemForm from '../../components/ItemForm';
import PrivateListNameEditor from '../../components/PrivateListNameEditForm';
import { postRequestProcess } from '../../utils';

interface Props {
    bookList: PrivateBookListModel;
    statuses: SelectListItem[];
    addItem: (listItem: PrivateBookListItem) => Promise<void>;
    updateListName: (name: string) => Promise<void>;
    deleteItem: (itemId: number) => Promise<void>;
    updateItem: (item: PrivateBookListItem) => Promise<void>;
    switchItemEditMode: (itemId: number) => void;
    switchListEditMode: () => void;
    getPrivateList: () => Promise<void>;
    getBookStatuses: () => Promise<void>;
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
        if(!this.props.bookList) {
            await this.props.getPrivateList();
        }

        if(!this.props.statuses) {
            await this.props.getBookStatuses();
         }
    }

    shouldComponentUpdate(nextProps: Props) {
        return nextProps.bookList != null && nextProps.statuses != null;
    }

    render() {
        let list;
        if(this.isDataLoaded()) {
            let listItems;
            if(this.props.bookList.items.length > 0) {
                listItems = this.props.bookList.items.map(listItem => {
                    const actions = [
                        {onClick: () => this.props.switchItemEditMode(listItem.id), text: 'Edit'},
                        {onClick: deleteItem(listItem, this.props.deleteItem), text: 'Delete'}
                    ];
                    const Contexed = withContextMenu(actions, PrivateBookLI);

                    return (
                        <Contexed
                            key={listItem.id}
                            listItem={listItem}
                            onSave={this.props.updateItem}
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
                        onSave={this.props.updateListName}
                        onCancel={this.props.switchListEditMode}
                    />
                ) : this.props.bookList.name
            );
            const bookListActions = [{onClick: this.props.switchListEditMode, text: 'Edit list name'}];
            const ContexedList = withContextMenu(bookListActions, PrivateBookUL);
            list = <ContexedList items={listItems} legend={legend} />;
        }
        return (
            <div>
                {
                    this.props.bookList && !this.props.bookList.isInEditMode
                        ? <ItemForm onSubmit={this.props.addItem} /> : null
                }
                {list}
            </div>
        );
    }

    private isDataLoaded() {
        return this.props.bookList != null && this.props.statuses != null;
    }
}

function mapStateToProps(state: RootState) {
    return {
        bookList: state.private.list,
        statuses: state.private.bookStatuses
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const bookService = new PrivateBookListService(dispatch);
    return {
        addItem: async (listItem: PrivateBookListItem) => {
            const result = await bookService.addItem(listItem);
            postRequestProcess(result);
        },
        deleteItem: async (itemId: number) => {
            const result = await bookService.removeItem(itemId);
            const castedResult = result as RequestResult<any>;
            if(castedResult) {
                postRequestProcess(castedResult);
            }
        },
        updateItem: async (item: PrivateBookListItem) => {
            const result = await bookService.updateItem(item);
            postRequestProcess(result);
        },
        switchItemEditMode: (itemId: number) => {
            dispatch(privateBookListAction.switchEditModeForItem(itemId));
        },
        getPrivateList: async () => {
            const result = await bookService.getList();
            postRequestProcess(result);
        },
        switchListEditMode: () => {
            dispatch(privateBookListAction.switchEditModeForList());
        },
        updateListName: async (newName: string) => {
            const result = await bookService.updateListName(newName);
            const castedResult = result as RequestResult<any>;
            if(castedResult) {
                postRequestProcess(castedResult);
            }
        },
        getBookStatuses: async () => {
            const result = await bookService.getBookStatuses();
            postRequestProcess(result);
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);