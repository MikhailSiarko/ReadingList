import { RootState } from '../index';
import initialState from '../initialState';
import { getType } from 'typesafe-actions';
import { BookListAction, bookListAction } from '../../actions/bookList';
import { PrivateBookListItem } from '../../../models/BookList/Implementations/PrivateBookListItem';
import { PrivateBookList } from '../../../models/BookList/Implementations/PrivateBookList';
import { cloneDeep } from 'lodash';

export function bookListReducer(state: RootState.PrivateList = initialState.privateList,
                                      action: BookListAction) {
    switch (action.type) {
        case getType(bookListAction.setPrivateList):
            return action.list;
        case getType(bookListAction.unsetPrivateList):
            return initialState.privateList;
        case getType(bookListAction.addItem):
            const copy = cloneDeep(state as PrivateBookList);
            copy.items.push(action.listItem);
            return copy;
        case getType(bookListAction.removeItem):
            let deepCopy = cloneDeep(state as PrivateBookList);
            const itemIndex = deepCopy.items
                .findIndex((listItem: PrivateBookListItem) => listItem.id === action.itemId);
            deepCopy.items.splice(itemIndex, 1);
            return deepCopy;
        case getType(bookListAction.updateItem):
            const clone = cloneDeep(state as PrivateBookList);
            const index = clone.items.findIndex(((listItem: PrivateBookListItem) => listItem.id === action.item.id));
            clone.items[index] = action.item;
            return clone;
        case getType(bookListAction.switchEditModeForItem):
            const deepClone = cloneDeep(state as PrivateBookList);
            deepClone.items.forEach((listItem: PrivateBookListItem) => {
                if(listItem.id === action.itemId) {
                    listItem.isOnEditMode = !listItem.isOnEditMode;
                }
            });
            return deepClone;
        default:
            return state;
    }
}