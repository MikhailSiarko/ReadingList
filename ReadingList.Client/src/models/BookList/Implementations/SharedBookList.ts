import { List } from '..';
import { SharedBookListItem } from './SharedBookListItem';

export class SharedBookList implements List<SharedBookListItem> {
    isInEditMode: boolean = false;
    name: string;
    id: number;
    items: SharedBookListItem[];
    type: number;
    tags: string[];
    booksCount: number;
}