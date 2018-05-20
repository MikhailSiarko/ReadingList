import * as React from 'react';
import { BookList } from '../../models/BookList/Implementations/BookList';
import { generateStatusSelectItems, BookStatus, BookStatusKey } from '../../models/BookList/Implementations/BookStatus';
import BookUL from '../../components/BookUL';
import BookLI from '../../components/BookUL/BookLI';
import ContextMenu from '../../components/ContextMenu/ContextMenu';

interface PrivateListProps {
    bookList: BookList;
    // add: (book: BookModel) => void;
    // remove: (id: string) => void;
    // changeStatus: (itemId: string, newStatus: BookStatus) => void;
}

interface PrivateListState {
    bookList: BookList;
}

class PrivateBookList extends React.Component<PrivateListProps, PrivateListState> {
    constructor(props: PrivateListProps) {
        super(props);
        this.state = { bookList: { ...props.bookList } };
    }
    changeHandler = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const index = this.state.bookList.items.findIndex((value) => value.id === event.target.dataset.itemId);
        const item = this.state.bookList.items[index];
        const copy = [...this.state.bookList.items];
        if(item) {
            const key = BookStatusKey[event.target.value];
            item.status = BookStatus[key];
            copy[index] = item;
        }
        const list = Object.assign({}, this.state.bookList, {items: copy});
        this.setState({ bookList: list });
    }

    removeItemHandler(id: string) {
        const copy = [...this.state.bookList.items];
        const index = copy.findIndex((value) => value.id === id);
        copy.splice(index, 1);
        const list = Object.assign({}, this.state.bookList, {items: copy});
        this.setState({ bookList: list });
    }

    render() {
        const statusOptions = generateStatusSelectItems();
        const options = statusOptions.map((item) =>
            <option key={item.value} value={item.value}>{item.text}</option>
        );
        const listItems = this.state.bookList.items.map((listItem) => (
            <ContextMenu element={'li'} key={'context-menu' + listItem.id}
                         menuItems={[{onClick:() => this.removeItemHandler(listItem.id), text: 'Remove'}]}>
                <BookLI key={listItem.id} listItem={listItem} element={'div'}
                        shouldStatusSelectorRender={true} options={options} onBookStatusChange={this.changeHandler} />
            </ContextMenu>
        ));
        return (
            <BookUL>
                {listItems}
            </BookUL>
        );
    }
}

export default PrivateBookList;