import { Action } from 'redux';
import { bookActions } from './actions';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '..';
import { BookService } from 'src/services';
import { takeLeading } from 'redux-saga/effects';
import { BookActionType } from './actionTypes';

function* findBooksAsync(action: Action) {
    if(isActionOf(bookActions.findBegin, action)) {
        yield executeAsync(
            () => new BookService().findBooks(action.payload.query, action.payload.chunk),
            bookActions.findSuccess,
            null,
            true
        );
    }
}

export function* watchBooksSaga() {
    yield takeLeading(BookActionType.FIND_BOOKS_BEGIN, findBooksAsync);
}