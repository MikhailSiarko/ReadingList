import { PrivateListService } from 'src/services';
import { executeAsync } from '../saga';
import { privateListActions } from '.';
import { put, takeLeading, all } from 'redux-saga/effects';
import { isActionOf } from 'typesafe-actions';
import { PrivateListActionType } from './actionTypes';
import { watchPrivateListItem } from './item';
import { watchItemStatuses } from './itemStatus';
import { Action } from 'redux';

function* fetchListAsync(action: Action) {
    if(isActionOf(privateListActions.fetchListBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().getList(),
            privateListActions.updateListSuccess,
            _ => put(privateListActions.fetchItemStatusesBegin()),
            true
        );
    }
}

function* updateListAsync(action: Action) {
    if(isActionOf(privateListActions.updateListBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().updateList(action.payload),
            privateListActions.updateListSuccess,
            null,
            true
        );
    }
}

function* shareListAsync(action: Action) {
    if(isActionOf(privateListActions.shareList, action)) {
        yield executeAsync(
            () => new PrivateListService().sharePrivateList(action.payload),
            null,
            null,
            true
        );
    }
}

function* watchPrivateList() {
    yield takeLeading(PrivateListActionType.FETCH_PRIVATE_LIST_BEGIN, fetchListAsync);
    yield takeLeading(PrivateListActionType.UPDATE_PRIVATE_LIST_BEGIN, updateListAsync);
    yield takeLeading(PrivateListActionType.SHARE_PRIVATE_LIST, shareListAsync);
}

export function* rootPrivateListSaga() {
    yield all([
        watchPrivateListItem(),
        watchItemStatuses(),
        watchPrivateList()
    ]);
}