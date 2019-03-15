import { Action } from 'redux';
import { bookActions } from './actions';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '..';
import { BookService } from 'src/services';
import { takeLeading, put } from 'redux-saga/effects';
import { BookActionType } from './actionTypes';
import { Chunked, Book } from 'src/models';

function* findBooksAsync(action: Action) {
    if(isActionOf(bookActions.findBegin, action)) {
        yield executeAsync(
            () => new BookService().findBooks(action.payload.query, action.payload.chunk),
            function* (books: Chunked<Book>) {
                yield put(bookActions.findSuccess(books));
            },
            true
        );
    }
}

function* shareBookAsync(action: Action) {
    if(isActionOf(bookActions.shareBook, action)) {
        yield executeAsync(
            () => new BookService().shareBook(action.payload.bookId, action.payload.lists),
            null,
            true
        );
    }
}

export function* watchBooksSaga() {
    yield takeLeading(BookActionType.FIND_BOOKS_BEGIN, findBooksAsync);
    yield takeLeading(BookActionType.SHARE_BOOK_BEGIN, shareBookAsync);
}