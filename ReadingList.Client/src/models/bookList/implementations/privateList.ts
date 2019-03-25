import { List } from '../abstracts/list';
import { PrivateBookListItem } from './privateListItem';

export class PrivateBookList extends List<PrivateBookListItem> {
    static switchItemMode(list: PrivateBookList, itemId: number) {
        list.items.forEach((listItem: PrivateBookListItem) => {
            if (listItem.id === itemId) {
                listItem.isInEditMode = !listItem.isInEditMode;
            }
        });
    }
}

export type PrivateListUpdateData = {
    name: string
};