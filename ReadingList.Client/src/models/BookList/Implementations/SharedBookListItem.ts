import { ListItem } from '..';

export class SharedBookListItem implements ListItem {
    id: number;
    isOnEditMode: boolean = false;
    author: string;
    title: string;
    genreId: string;
    tags: string[];
    listId: number;
    bookId: number;
    
    constructor(author: string, title: string, tags: string[]) {
        this.author = author;
        this.title = title;
        this.tags = tags;
    }
}