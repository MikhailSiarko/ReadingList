import { List } from '../Abstractions/List';
import { PrivateBookListItem } from './PrivateBookListItem';

export class PrivateBookList implements List<PrivateBookListItem> {
    name: string;
    id: number;
    items: PrivateBookListItem[];
    type: number;
}