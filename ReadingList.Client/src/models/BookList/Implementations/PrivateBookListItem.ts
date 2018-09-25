import { ListItem } from '../Abstractions/ListItem';

export class PrivateBookListItemModel implements ListItem {
    id: number;
    status: string;
    isOnEditMode: boolean = false;
    author: string;
    title: string;
    genreId: string;
    readingTimeInSeconds: number;
}