import { RootState } from '../index';
import initialState from '../initialState';
import { getType } from 'typesafe-actions';
import { BookListAction, bookListAction } from '../../actions/bookList';
import { BookListItem } from '../../../models/BookList/Implementations/BookListItem';
import { guid } from '../../../utils';
import { BookList } from '../../../models/BookList/Implementations/BookList';

export function bookListReducer(state: RootState.PrivateList = initialState.privateList,
                                      action: BookListAction) {
    let copy = undefined;
    switch (action.type) {
        case getType(bookListAction.add):
            const item = new BookListItem(guid(), action.book);
            copy = {...state} as BookList;
            copy.items.push(item);
            return copy;
        case getType(bookListAction.remove):
            copy = {...state} as BookList;
            const itemIndex = copy.items.findIndex((listItem: BookListItem) => listItem.id === action.itemId);
            copy.items.splice(itemIndex, 1);
            return copy;
        case getType(bookListAction.update):
            copy = {...state} as BookList;
            copy.items.forEach((listItem: BookListItem) => {
                if(listItem.id === action.item.id) {
                    listItem = {
                        ...action.item
                    } as BookListItem;
                }
            });
            return copy;
        case getType(bookListAction.edit):
            copy = {...state} as BookList;
            copy.items.forEach((listItem: BookListItem) => {
                if(listItem.id === action.itemId) {
                    listItem.isOnEditMode = true;
                }
            });
            return copy;
        default:
            return state;
    }
}