import * as React from 'react';
import { BookList } from '../../models/BookList/Implementations/BookList';
import { generateStatusSelectItems, BookStatus, BookStatusKey } from '../../models/BookList/Implementations/BookStatus';
import Layout from '../../components/Layout';

interface PrivateListProps {
    bookList: BookList;
    // add: (book: BookModel) => void;
    // remove: (itemId: string) => void;
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
        console.log(event.target);
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
    render() {
        const statusOptions = generateStatusSelectItems();
        const options = statusOptions.map((item) => {
            return <option key={item.value} value={item.value}>{item.text}</option>;
        });
        const lis = this.state.bookList.items.map((listItem) => {
            return (
                <li key={listItem.id}>
                    <div className={'book-info'}>
                        <h5 className={'book-title'}>
                            <q>{listItem.data.title}</q> by {listItem.data.author}
                        </h5>
                    </div>
                    <div className={'status-selector'}>
                        <p>Status:</p>
                        <select value={listItem.status}
                                name="BookStatus" data-item-id={listItem.id} onChange={this.changeHandler}>
                            {options}
                        </select>
                    </div>
                </li>
            );
        });
        return (
            <Layout element={'ul'} className={'book-list'}>
                {lis}
            </Layout>
        );
    }
}

export default PrivateBookList;