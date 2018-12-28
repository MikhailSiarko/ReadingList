import { List } from '..';
import { SharedBookListItem } from './SharedBookListItem';
import { Tag } from '../../Tag';
import { Moderator } from '../../Moderator';

export class SharedBookList implements List<SharedBookListItem> {
    isInEditMode: boolean = false;
    name: string;
    id: number;
    items: SharedBookListItem[];
    moderators: Moderator[];
    type: number;
    tags: Tag[];
    booksCount: number;
    editable: boolean;
    canBeModerated: boolean;
}