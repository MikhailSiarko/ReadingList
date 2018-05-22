import * as React from 'react';
import { generateStatusSelectItems } from '../../models/BookList/Implementations/BookStatus';
import BookUL from '../../components/BookUL';
import BookLI from '../../components/BookUL/BookLI';
import ContextMenu from '../../components/ContextMenu/ContextMenu';
import { BookModel } from '../../models/BookModel';
import { connect, Dispatch } from 'react-redux';
import { RootState } from '../../store/reducers';
import { bookListAction } from '../../store/actions/bookList';
import { BookListItem } from '../../models/BookList/Implementations/BookListItem';
import { RouteComponentProps } from 'react-router';

interface PrivateListProps extends RouteComponentProps<any> {
    bookList: RootState.PrivateList;
    add: (book: BookModel) => void;
    remove: (itemId: string) => void;
    update: (item: BookListItem) => void;
    switchEditMode: (itemId: string) => void;
}

class PrivateBookList extends React.Component<PrivateListProps> {
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
                       <BookLI key={listItem.id} listItem={listItem} element={'li'}
                               shouldStatusSelectorRender={true} options={options}
                               onSave={this.props.update} id={bookListItemId}
                               contextMenu={contextMenu} onCancel={this.props.switchEditMode} />
                );
           });
        }
        return (
                <BookUL>
                    {listItems}
                </BookUL>
            );
    }
}

function mapStateToProps(state: RootState) {
    return {
        bookList: state.privateList
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        add: (book: BookModel) => {
            dispatch(bookListAction.add(book));
        },
        remove: (itemId: string) => {
            dispatch(bookListAction.remove(itemId));
        },
        update: (item: BookListItem) => {
            dispatch(bookListAction.update(item));
        },
        switchEditMode: (itemId: string) => {
            dispatch(bookListAction.switchEditMode(itemId));
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);