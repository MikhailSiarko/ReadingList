import { ListItem } from '../abstracts/listItem';

export class SharedBookListItem implements ListItem {
    id: number;
    isInEditMode: boolean = false;
    author: string;
    title: string;
    genre: string;
    tags: string[];
    listId: number;
    bookId: number;

    constructor(author: string, title: string) {
        this.author = author;
        this.title = title;
    }
}