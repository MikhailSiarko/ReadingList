import { executeAsync } from '../../saga';
import { takeLeading } from 'redux-saga/effects';
import { privateListActions } from '..';
import { PrivateListItemStatusActionType } from './actionTypes';
import { Action } from 'redux';
import { isActionOf } from 'typesafe-actions';
import { itemStatusActions } from '.';
import { PrivateListService } from 'src/services';

function* fetchStatusesAsync(action: Action) {
    if(isActionOf(itemStatusActions.fetchItemStatusesBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().getItemStatuses(),
            privateListActions.fetchItemStatusesSuccess
        );
    }
}

export function* watchItemStatuses() {
    yield takeLeading(PrivateListItemStatusActionType.FETCH_PRIVATE_ITEM_STATUSES_BEGIN, fetchStatusesAsync);
}