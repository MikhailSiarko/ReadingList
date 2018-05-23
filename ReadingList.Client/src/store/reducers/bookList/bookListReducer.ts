import { RootState } from '../index';
import initialState from '../initialState';
import { getType } from 'typesafe-actions';
import { BookListAction, bookListAction } from '../../actions/bookList';
import { BookListItem } from '../../../models/BookList/Implementations/BookListItem';
import { BookList } from '../../../models/BookList/Implementations/BookList';
import { cloneDeep } from 'lodash';

export function bookListReducer(state: RootState.PrivateList = initialState.privateList,
                                      action: BookListAction) {
    switch (action.type) {
        case getType(bookListAction.add):
            const copy = cloneDeep(state as BookList);
            copy.items.push(action.listItem);
            return copy;
        case getType(bookListAction.remove):
            let deepCopy = cloneDeep(state as BookList);
            const itemIndex = deepCopy.items
                .findIndex((listItem: BookListItem) => listItem.id === action.itemId);
            deepCopy.items.splice(itemIndex, 1);
            return deepCopy;
        case getType(bookListAction.update):
            const clone = cloneDeep(state as BookList);
            const index = clone.items.findIndex(((listItem: BookListItem) => listItem.id === action.item.id));
            clone.items[index] = action.item;
            return clone;
        case getType(bookListAction.switchEditMode):
            const deepClone = cloneDeep(state as BookList);
            deepClone.items.forEach((listItem: BookListItem) => {
                if(listItem.id === action.itemId) {
                    listItem.isOnEditMode = !listItem.isOnEditMode;
                }
            });
            return deepClone;
        default:
            return state;
    }
}