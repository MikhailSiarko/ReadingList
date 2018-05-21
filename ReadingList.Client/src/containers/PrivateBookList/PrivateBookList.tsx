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
    edit: (itemId: string) => void;
}

class PrivateBookList extends React.Component<PrivateListProps> {
    // changeHandler = (event: React.ChangeEvent<HTMLSelectElement>) => {
    //     if(this.props.bookList) {
    //         const index = this.props.bookList.items.findIndex((value) => value.id === event.target.dataset.itemId);
    //         const item = this.props.bookList.items[index];
    //         const copy = [...this.props.bookList.items];
    //         if(item) {
    //             const key = BookStatusKey[event.target.value];
    //             item.status = BookStatus[key];
    //             copy[index] = item;
    //         }
    //         const list = Object.assign({}, this.props.bookList, {items: copy});
    //         this.setState({ bookList: list });
    //     }
    // }

    removeItemHandler(id: string) {
        if(this.props.bookList) {
            const copy = [...this.props.bookList.items];
            const index = copy.findIndex((value) => value.id === id);
            copy.splice(index, 1);
            const list = Object.assign({}, this.props.bookList, {items: copy});
            this.setState({ bookList: list });
        }
    }

    render() {
        const statusOptions = generateStatusSelectItems();
        const options = statusOptions.map((item) =>
            <option key={item.value} value={item.value}>{item.text}</option>
        );
        return this.props.bookList 
            ? (
                <BookUL>
                    {
                         this.props.bookList.items.map((listItem) => {
                             return (
                                <ContextMenu element={'li'} key={'context-menu' + listItem.id}
                                        menuItems={[
                                                {onClick: () => this.props.remove(listItem.id), text: 'Remove'},
                                                {onClick: () => this.props.edit(listItem.id), text: 'Edit'}
                                            ]}>
                                    <BookLI key={listItem.id} listItem={listItem} element={'div'}
                                            shouldStatusSelectorRender={true} options={options}
                                            onItemChange={() => console.log('He')}
                                            isInEditMode={listItem.isOnEditMode} />
                                </ContextMenu>
                             );
                        })
                    }
                </BookUL>
            ) : null;
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
        edit: (itemId: string) => {
            dispatch(bookListAction.edit(itemId));
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);