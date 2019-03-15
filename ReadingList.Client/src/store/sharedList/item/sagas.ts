import { SharedListItemActionType } from './actionTypes';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '../../saga';
import { takeLeading, put } from 'redux-saga/effects';
import { Action } from 'redux';
import { BookService, SharedListService } from '../../../services';
import { sharedListActions } from '..';
import { SharedBookListItem } from '../../../models';

function* addItemAsync(action: Action) {
    if(isActionOf(sharedListActions.addItemBegin, action)) {
        yield executeAsync(
            () => new SharedListService().addItem(action.payload.listId, action.payload.bookId),
            function* (item: SharedBookListItem) {
                yield put(sharedListActions.addItemSuccess(item));
            },
            true
        );
    }
}

function* removeItemAsync(action: Action) {
    if(isActionOf(sharedListActions.deleteItemBegin, action)) {
        yield executeAsync(
            () => new SharedListService().deleteItem(action.payload.listId, action.payload.itemId),
            function* (itemId: number) {
                yield put(sharedListActions.deleteItemSuccess(itemId));
            },
            true
        );
    }
}

function* shareItemAsync(action: Action) {
    if(isActionOf(sharedListActions.shareItem, action)) {
        yield executeAsync(
            () => new BookService().shareBook(action.payload.bookId, action.payload.lists),
            null,
            true
        );
    }
}

export function* watchSharedListItem() {
    yield takeLeading(SharedListItemActionType.ADD_SHARED_ITEM_BEGIN, addItemAsync);
    yield takeLeading(SharedListItemActionType.DELETE_SHARED_ITEM_BEGIN, removeItemAsync);
    yield takeLeading(SharedListItemActionType.SHARE_SHARED_ITEM, shareItemAsync);
}