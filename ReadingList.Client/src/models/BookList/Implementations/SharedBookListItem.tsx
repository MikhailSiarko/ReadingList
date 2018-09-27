import { ListItem } from '../Abstractions/ListItem';

export class SharedBookListItem implements ListItem {
    id: number;
    isOnEditMode: boolean = false;
    author: string;
    title: string;
    genreId: string;
    tags: string[];
}