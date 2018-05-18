import { List } from '../Abstractions/List';
import { BookListItem } from './BookListItem';
import { ListType } from '../Abstractions/ListType';

export class BookList implements List<BookListItem> {
    id: string;
    items: BookListItem[];
    description: string;
    tags: string[];
    type: ListType;
}