import { List } from '..';
import { SharedBookListItem } from './sharedBookListItem';
import { Tag } from '../../tag';
import { Moderator } from '../../moderator';

export class SharedBookList extends List<SharedBookListItem> {
    moderators: Moderator[];
    tags: Tag[];
    booksCount: number;
    editable: boolean;
    canBeModerated: boolean;
}

export type SharedListUpdateData = {
    name: string,
    tags: Tag[],
    moderators: number[] | null
};

export type SharedListCreateData = {
    name: string,
    tags: Tag[]
};