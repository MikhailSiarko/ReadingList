import { BookActionType } from './actionTypes';
import { ActionType, createAction } from 'typesafe-actions';
import { Chunked, Book } from 'src/models';

export const bookActions = {
    findBegin: createAction(
        BookActionType.FIND_BOOKS_BEGIN,
        action => (query: string, chunk: number | null) => action({query, chunk})
    ),
    findSuccess: createAction(
        BookActionType.FIND_BOOKS_SUCCESS,
        action => (books: Chunked<Book>) => action(books)
    )
};

export type BookActions = ActionType<typeof bookActions>;