import { List } from '..';
import { PrivateBookListItem } from './PrivateBookListItem';

export class PrivateBookList implements List<PrivateBookListItem> {
    isInEditMode: boolean = false;
    name: string;
    id: number;
    items: PrivateBookListItem[];
    type: number;
}