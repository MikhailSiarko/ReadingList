import { List } from '..';
import { SharedBookListItem } from './SharedBookListItem';
import { Tag } from '../../Tag';

export class SharedBookList implements List<SharedBookListItem> {
    isInEditMode: boolean = false;
    name: string;
    id: number;
    items: SharedBookListItem[];
    type: number;
    tags: Tag[];
    booksCount: number;
    editable: boolean;
}