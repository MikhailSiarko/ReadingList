import { privateListActions } from '..';
import { PrivateListItemActionType } from './actionTypes';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from 'src/store/saga';
import { takeLeading } from 'redux-saga/effects';
import { Action } from 'redux';
import { BookService, PrivateListService } from 'src/services';

const bookService = new BookService();

function* addItemAsync(action: Action) {
    if(isActionOf(privateListActions.addItemBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().addItem(action.payload),
            privateListActions.addItemSuccess,
            null,
            true
        );
    }
}

function* updateItemAsync(action: Action) {
    if(isActionOf(privateListActions.updateItemBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().updateItem(action.payload),
            privateListActions.updateItemSuccess,
            null,
            true
        );
    }
}

function* removeItemAsync(action: Action) {
    if(isActionOf(privateListActions.removeItemBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().removeItem(action.payload),
            privateListActions.removeItemSuccess,
            null,
            true
        );
    }
}

function* shareItemAsync(action: Action) {
    if(isActionOf(privateListActions.shareItem, action)) {
        yield executeAsync(
            () => bookService.shareBook(action.payload.bookId, action.payload.lists),
            null,
            null,
            true
        );
    }
}

export function* watchPrivateListItem() {
    yield takeLeading(PrivateListItemActionType.ADD_PRIVATE_ITEM_BEGIN, addItemAsync);
    yield takeLeading(PrivateListItemActionType.UPDATE_PRIVATE_ITEM_BEGIN, updateItemAsync);
    yield takeLeading(PrivateListItemActionType.REMOVE_PRIVATE_ITEM_BEGIN, removeItemAsync);
    yield takeLeading(PrivateListItemActionType.SHARE_PRIVATE_ITEM, shareItemAsync);
}