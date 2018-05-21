import { RouterState } from 'connected-react-router';
import { ListType } from '../../models/BookList/Abstractions/ListType';
import { BookListItem } from '../../models/BookList/Implementations/BookListItem';
import { BookModel } from '../../models/BookModel';
import { BookList } from '../../models/BookList/Implementations/BookList';
import { BookStatus } from '../../models/BookList/Implementations/BookStatus';
import { guid } from '../../utils';

const bookList = {
    id: '1',
    type: ListType.Private,
    items: [
        new BookListItem(guid(), 
            {id: guid(), title: 'Martin Eden', author: 'Jack London'} as BookModel, BookStatus.Reading),
        new BookListItem(guid(), {id: guid(), title: 'Three comrades', author: 'Erich Maria Remark'} as BookModel)
    ]
} as BookList;

export default {
    router: { } as RouterState,
    identity: {
        isAuthenticated: sessionStorage.getItem('token') !== null,
        user: undefined
    },
    loading: false,
    privateList: bookList
};