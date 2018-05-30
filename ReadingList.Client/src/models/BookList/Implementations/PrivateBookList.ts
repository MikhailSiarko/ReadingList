import { List } from '../Abstractions/List';
import { PrivateBookListItemModel } from './PrivateBookListItem';

export class PrivateBookListModel implements List<PrivateBookListItemModel> {
    isInEditMode: boolean = false;
    name: string;
    id: number;
    items: PrivateBookListItemModel[];
    type: number;
}