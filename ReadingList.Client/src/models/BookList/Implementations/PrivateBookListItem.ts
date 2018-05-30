import { ListItem } from '../Abstractions/ListItem';

export class PrivateBookListItemModel implements ListItem {
    readingTimeInTicks: number;
    id: number;
    status: string;
    isOnEditMode: boolean = false;
    author: string;
    title: string;
}