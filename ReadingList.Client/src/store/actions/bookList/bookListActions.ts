import { createAction, getReturnOfExpression } from 'typesafe-actions';
import { BookListActionType } from './BookListActionType';
import { BookModel } from '../../../models/BookModel';
import { BookListItem } from '../../../models/BookList/Implementations/BookListItem';

export const bookListAction = {
    add: createAction(BookListActionType.ADD_ITEM, (book: BookModel) => {
        return {
            type: BookListActionType.ADD_ITEM,
            book
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
    edit: createAction(BookListActionType.EDIT_ITEM, (itemId: string) => {
        return {
            type: BookListActionType.EDIT_ITEM,
            itemId
        };
    })
};

const returnOfActions = Object.values(bookListAction).map(getReturnOfExpression);
export type BookListAction = typeof returnOfActions[number];