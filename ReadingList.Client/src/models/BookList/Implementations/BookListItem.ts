import { ListItem } from '../Abstractions/ListItem';
import { BookModel } from '../../BookModel';
import { BookStatus, BookStatusKey } from './BookStatus';

export class BookListItem implements ListItem<BookModel> {
    readingTime: number;
    id: string;
    data: BookModel;
    status: string;
    isOnEditMode: boolean;
    constructor(id: string, book: BookModel, status: string = BookStatus[BookStatusKey.ToReading],
            readingTime: number = 0, isOnEditMode: boolean = false) {
        this.id = id;
        this.data = book;
        this.status = status;
        this.isOnEditMode = isOnEditMode;
        this.readingTime = readingTime;
    }
}