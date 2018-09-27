import { List } from '../Abstractions/List';
import { PrivateBookListItem } from './PrivateBookListItem';

export class PrivateBookListModel implements List<PrivateBookListItem> {
    isInEditMode: boolean = false;
    name: string;
    id: number;
    items: PrivateBookListItem[];
    type: number;
}