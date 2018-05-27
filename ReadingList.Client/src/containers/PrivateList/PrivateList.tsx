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
import { BookListService } from '../../services/BookListService';
import { RequestResult } from '../../models/Request';
import { PrivateBookList } from '../../models/BookList/Implementations/PrivateBookList';

interface PrivateListProps extends RouteComponentProps<any> {
    bookList: RootState.PrivateList;
    add: (listItem: PrivateBookListItem) => void;
    remove: (itemId: number) => void;
    update: (item: PrivateBookListItem) => void;
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
        if(this.props.bookList) {
            listItems = this.props.bookList.items.map((listItem) => {
                const bookListItemId = 'bookListItem' + listItem.id;
                const contextMenu = (() => (
                   <ContextMenu rootId={bookListItemId} menuItems={
                           [
                               {onClick: () => this.props.switchEditMode(listItem.id), text: 'Edit'},
                               {onClick: () => this.props.remove(listItem.id), text: 'Remove'}
                           ]
                       } />))();
                return (
                       <BookLI key={listItem.id} listItem={listItem}
                               shouldStatusSelectorRender={true} options={options}
                               onSave={this.props.update} id={bookListItemId}
                               contextMenu={contextMenu} onCancel={this.props.switchEditMode} />
                );
           });
        }
        return (
                <div>
                    <ItemForm onSubmit={this.props.add} />
                    <BookUL>
                        {listItems}
                    </BookUL>
                </div>
            );
    }
}

function postRequestProcess(result: RequestResult<PrivateBookList>) {
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
    const bookService = new BookListService();
    return {
        add: (listItem: PrivateBookListItem) => {
            dispatch(bookListAction.addItem(listItem));
        },
        remove: (itemId: number) => {
            dispatch(bookListAction.removeItem(itemId));
        },
        update: (item: PrivateBookListItem) => {
            dispatch(bookListAction.updateItem(item));
        },
        switchEditMode: (itemId: number) => {
            dispatch(bookListAction.switchEditModeForItem(itemId));
        },
        getPrivateList: async () => {
            const result = await bookService.getPrivateList(dispatch);
            postRequestProcess(result);
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateList);