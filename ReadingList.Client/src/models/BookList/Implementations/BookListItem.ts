import { ListItem } from '../Abstractions/ListItem';
import { BookModel } from '../../BookModel';
import { BookStatus, BookStatusKey } from './BookStatus';

export class BookListItem implements ListItem<BookModel> {
    id: string;
    data: BookModel;
    status: string;
    constructor(id: string, book: BookModel, status: string = BookStatus[BookStatusKey.ToRead]) {
        this.id = id;
        this.data = book;
        this.status = status;
    }
}