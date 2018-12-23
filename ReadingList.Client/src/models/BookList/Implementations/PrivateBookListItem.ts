import { ListItem } from '..';

export class PrivateBookListItem implements ListItem {
    id: number;
    status: number;
    isInEditMode: boolean = false;
    author: string;
    title: string;
    genreId: string;
    readingTimeInSeconds: number;
    listId: number;
    bookId: number;
}