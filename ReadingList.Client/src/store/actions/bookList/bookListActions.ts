import { createAction, getReturnOfExpression } from 'typesafe-actions';
import { BookListActionType } from './BookListActionType';
import { BookListItem } from '../../../models/BookList/Implementations/BookListItem';

export const bookListAction = {
    add: createAction(BookListActionType.ADD_ITEM, (listItem: BookListItem) => {
        return {
            type: BookListActionType.ADD_ITEM,
            listItem
        };
    }),
    remove: createAction(BookListActionType.REMOVE_ITEM, (itemId: string) => {
        return {
            type: BookListActionType.REMOVE_ITEM,
            itemId
        };
    }),
    update: createAction(BookListActionType.UPDATE_ITEM, (item: BookListItem) => {
        return {
            type: BookListActionType.UPDATE_ITEM,
            item
        };
    }),
    switchEditMode: createAction(BookListActionType.SWITCH_EDIT_MODE, (itemId: string) => {
        return {
            type: BookListActionType.SWITCH_EDIT_MODE,
            itemId
        };
    })
};

const returnOfActions = Object.values(bookListAction).map(getReturnOfExpression);
export type BookListAction = typeof returnOfActions[number];