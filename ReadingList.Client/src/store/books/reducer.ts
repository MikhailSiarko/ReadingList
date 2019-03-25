import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { BookActions, bookActions } from './actions';

export function bookReducer(state: RootState.Books = initialState.books, action: BookActions) {
    switch (action.type) {
        case getType(bookActions.findSuccess):
            return action.payload;
        case getType(bookActions.clearBookState):
            return initialState.books;
        default:
            return state;
    }
}