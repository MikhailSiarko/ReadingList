import { privateListActions } from '..';
import { PrivateListItemActionType } from './actionTypes';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '../../saga';
import { takeLeading, put } from 'redux-saga/effects';
import { Action } from 'redux';
import { PrivateListService } from '../../../services';
import { PrivateBookListItem } from '../../../models';

function* addItemAsync(action: Action) {
    if(isActionOf(privateListActions.addItemBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().addItem(action.payload),
            function* (item: PrivateBookListItem) {
                yield put(privateListActions.addItemSuccess(item));
            },
            true
        );
    }
}

function* updateItemAsync(action: Action) {
    if(isActionOf(privateListActions.updateItemBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().updateItem(action.payload.itemId, action.payload.data),
            function* (item: PrivateBookListItem) {
                yield put(privateListActions.updateItemSuccess(item));
            },
            true
        );
    }
}

function* deleteItemAsync(action: Action) {
    if(isActionOf(privateListActions.deleteItemBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().deleteItem(action.payload),
            function* (itemId: number) {
                yield put(privateListActions.deleteItemSuccess(itemId));
            },
            true
        );
    }
}

export function* watchPrivateListItem() {
    yield takeLeading(PrivateListItemActionType.ADD_PRIVATE_ITEM_BEGIN, addItemAsync);
    yield takeLeading(PrivateListItemActionType.UPDATE_PRIVATE_ITEM_BEGIN, updateItemAsync);
    yield takeLeading(PrivateListItemActionType.DELETE_PRIVATE_ITEM_BEGIN, deleteItemAsync);
}