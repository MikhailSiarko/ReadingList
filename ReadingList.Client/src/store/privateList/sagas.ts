import { PrivateListService } from '../../services';
import { executeAsync } from '../saga';
import { privateListActions } from '.';
import { put, takeLeading, all } from 'redux-saga/effects';
import { isActionOf } from 'typesafe-actions';
import { PrivateListActionType } from './actionTypes';
import { watchPrivateListItem } from './item/sagas';
import { watchItemStatuses } from './itemStatus/sagas';
import { Action } from 'redux';
import { PrivateBookList } from '../../models';

function* fetchListAsync(action: Action) {
    if(isActionOf(privateListActions.fetchListBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().getList(),
            function* (list: PrivateBookList) {
                yield put(privateListActions.updateListSuccess(list));
                yield put(privateListActions.fetchItemStatusesBegin());
            },
            true
        );
    }
}

function* updateListAsync(action: Action) {
    if(isActionOf(privateListActions.updateListBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().updateList(action.payload),
            function* (list: PrivateBookList) {
                yield put(privateListActions.updateListSuccess(list));
            },
            true
        );
    }
}

function* shareListAsync(action: Action) {
    if(isActionOf(privateListActions.shareList, action)) {
        yield executeAsync(
            () => new PrivateListService().sharePrivateList(action.payload),
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