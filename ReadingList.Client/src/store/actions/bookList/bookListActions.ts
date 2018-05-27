import { createAction, getReturnOfExpression } from 'typesafe-actions';
import { BookListActionType } from './BookListActionType';
import { PrivateBookListItem } from '../../../models/BookList/Implementations/PrivateBookListItem';
import { PrivateBookList } from '../../../models/BookList/Implementations/PrivateBookList';

export const bookListAction = {
    setPrivateList: createAction(BookListActionType.SET_PRIVATE_LIST, (list: PrivateBookList) => {
        return {
            type: BookListActionType.SET_PRIVATE_LIST,
            list
        };
    }),
    unsetPrivateList: createAction(BookListActionType.UNSET_PRIVATE_LIST, () => {
        return {
            type: BookListActionType.UNSET_PRIVATE_LIST,
        };
    }),
    addItem: createAction(BookListActionType.ADD_ITEM, (listItem: PrivateBookListItem) => {
        return {
            type: BookListActionType.ADD_ITEM,
            listItem
        };
    }),
    removeItem: createAction(BookListActionType.REMOVE_ITEM, (itemId: number) => {
        return {
            type: BookListActionType.REMOVE_ITEM,
            itemId
        };
    }),
    updateItem: createAction(BookListActionType.UPDATE_ITEM, (item: PrivateBookListItem) => {
        return {
            type: BookListActionType.UPDATE_ITEM,
            item
        };
    }),
    switchEditModeForItem: createAction(BookListActionType.SWITCH_EDIT_MODE, (itemId: number) => {
        return {
            type: BookListActionType.SWITCH_EDIT_MODE,
            itemId
        };
    })
};

const returnOfActions = Object.values(bookListAction).map(getReturnOfExpression);
export type BookListAction = typeof returnOfActions[number];