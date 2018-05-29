import * as React from 'react';
import { generateStatusSelectItems } from '../../models/BookList/Implementations/BookStatus';
import BookUL from '../../components/BookUL';
import BookLI from '../../components/BookUL/BookLI';
import ContextMenu from '../../components/ContextMenu/ContextMenu';
import { connect, Dispatch } from 'react-redux';
import { RootState } from '../../store/reducers';
import { bookListAction } from '../../store/actions/bookList';
import { PrivateBookListItem } from '../../models/BookList/Implementations/PrivateBookListItem';
import { RouteComponentProps } from 'react-router';
import ItemForm from '../../components/BookUL/ItemForm';
import { PrivateBookListService } from '../../services/PrivateBookListService';
import { RequestResult } from '../../models/Request';

interface PrivateListProps extends RouteComponentProps<any> {
    bookList: RootState.PrivateList;
    addItem: (listItem: PrivateBookListItem) => Promise<void>;
    removeItem: (itemId: number) => Promise<void>;
    updateItem: (item: PrivateBookListItem) => Promise<void>;
    switchEditMode: (itemId: number) => void;
    getPrivateList: () => Promise<void>;
}

class PrivateList extends React.Component<PrivateListProps> {
    async componentDidMount() {
        if(!this.props.bookList) {
            await this.props.getPrivateList();
        }
    }
    render() {
        const statusOptions = generateStatusSelectItems();
        const options = statusOptions.map((item) =>
            <option key={item.value} value={item.value}>{item.text}</option>
        );
        let listItems;
        let listName = '';
        if(this.props.bookList) {
            listName = this.props.bookList.name;
            if(this.props.bookList.items.length > 0) {
                listItems = this.props.bookList.items.map((listItem) => {
                    const bookListItemId = 'bookListItem' + listItem.id;
                    const contextMenu = (() => (
                    <ContextMenu rootId={bookListItemId} menuItems={
                            [
                                {onClick: () => this.props.switchEditMode(listItem.id), text: 'Edit'},
                                {onClick: () => this.props.removeItem(listItem.id), text: 'Remove'}
                            ]
                        } />))();
                    return (
                        <BookLI key={listItem.id} listItem={listItem}
                                shouldStatusSelectorRender={true} options={options}
                                onSave={this.props.updateItem} id={bookListItemId}
                                contextMenu={contextMenu} onCancel={this.props.switchEditMode} />
                    );
                });
            } else {
                listItems = <h3>Here are no books yet</h3>;
            }
        }
        return (
                <div>
                    <ItemForm onSubmit={this.props.addItem} />
                    <BookUL listName={listName}>
                        {listItems}
                    </BookUL>
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
        bookList: state.privateList
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const bookService = new PrivateBookListService();
    return {
        addItem: async (listItem: PrivateBookListItem) => {
            const result = await bookService.addItem(dispatch, listItem);
            postRequestProcess(result);
        },
        removeItem: async (itemId: number) => {
            const result = await bookService.removeItem(dispatch, itemId);
            const castedResult = result as RequestResult<any>;
            if(!castedResult) {
                postRequestProcess(castedResult);
            }
        },
        updateItem: async (item: PrivateBookListItem) => {
            const result = await bookService.updateItem(dispatch, item);
            postRequestProcess(result);
        },
        switchEditMode: (itemId: number) => {
            dispatch(bookListAction.switchEditModeForItem(itemId));
        },
        getPrivateList: async () => {
            const result = await bookService.getList(dispatch);
            postRequestProcess(result);
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateList);