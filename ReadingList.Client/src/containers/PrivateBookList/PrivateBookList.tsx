import * as React from 'react';
import { RootState } from '../../store/reducers';
import { PrivateBookListItemModel, RequestResult, SelectListItem, PrivateBookListModel } from '../../models';
import PrivateBookLI from '../../components/PrivateBookLI';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService } from '../../services';
import { withContextMenu } from '../../hoc/withContextMenu';
import PrivateBookUL from '../../components/PrivateBookUL';
import ItemForm from '../../components/ItemForm';
import PrivateListNameEditor from '../../components/PrivateListNameEditForm';

interface Props {
    bookList: PrivateBookListModel;
    statuses: SelectListItem[];
    addItem: (listItem: PrivateBookListItemModel) => Promise<void>;
    updateListName: (name: string) => Promise<void>;
    removeItem: (itemId: number) => Promise<void>;
    updateItem: (item: PrivateBookListItemModel) => Promise<void>;
    switchItemEditMode: (itemId: number) => void;
    switchListEditMode: () => void;
    getPrivateList: () => Promise<void>;
    getBookStatuses: () => Promise<void>;
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

    render() {
        let list;
        if(this.props.bookList) {
            let listItems;
            if(this.props.bookList.items.length > 0) {
                listItems = this.props.bookList.items.map(listItem => {
                    const actions = [
                        {onClick: () => this.props.switchItemEditMode(listItem.id), text: 'Edit'},
                        {onClick: () => this.props.removeItem(listItem.id), text: 'Remove'}
                    ];
                    const Contexed = withContextMenu(actions, PrivateBookLI);

                    let options;

                    if(this.props.statuses) {
                        options = this.props.statuses.map(item =>
                            <option key={item.value} value={item.value}>{item.text}</option>
                        );
                    } else {
                        options = [
                            <option key={0} value={0} />
                        ];
                    }

                    return (
                        <Contexed
                            key={listItem.id}
                            listItem={listItem}
                            options={options}
                            onSave={this.props.updateItem}
                            onCancel={this.props.switchItemEditMode}
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
}

function postRequestProcess(result: RequestResult<any>) {
    if(!result.isSucceed) {
        alert(result.errorMessage);
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
        addItem: async (listItem: PrivateBookListItemModel) => {
            const result = await bookService.addItem(listItem);
            postRequestProcess(result);
        },
        removeItem: async (itemId: number) => {
            const result = await bookService.removeItem(itemId);
            const castedResult = result as RequestResult<any>;
            if(castedResult) {
                postRequestProcess(castedResult);
            }
        },
        updateItem: async (item: PrivateBookListItemModel) => {
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