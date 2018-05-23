import * as React from 'react';
import { generateStatusSelectItems } from '../../models/BookList/Implementations/BookStatus';
import BookUL from '../../components/BookUL';
import BookLI from '../../components/BookUL/BookLI';
import ContextMenu from '../../components/ContextMenu/ContextMenu';
import { connect, Dispatch } from 'react-redux';
import { RootState } from '../../store/reducers';
import { bookListAction } from '../../store/actions/bookList';
import { BookListItem } from '../../models/BookList/Implementations/BookListItem';
import { RouteComponentProps } from 'react-router';
import ItemForm from '../../components/BookUL/ItemForm';

interface PrivateListProps extends RouteComponentProps<any> {
    bookList: RootState.PrivateList;
    add: (listItem: BookListItem) => void;
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

function mapStateToProps(state: RootState) {
    return {
        bookList: state.privateList
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        add: (listItem: BookListItem) => {
            dispatch(bookListAction.add(listItem));
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