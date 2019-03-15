import { executeAsync } from '../../saga';
import { takeLeading, put } from 'redux-saga/effects';
import { privateListActions } from '..';
import { PrivateListItemStatusActionType } from './actionTypes';
import { Action } from 'redux';
import { isActionOf } from 'typesafe-actions';
import { itemStatusActions } from './actions';
import { PrivateListService } from 'src/services';
import { SelectListItem } from 'src/models';

function* fetchStatusesAsync(action: Action) {
    if(isActionOf(itemStatusActions.fetchItemStatusesBegin, action)) {
        yield executeAsync(
            () => new PrivateListService().getItemStatuses(),
            function* (statuses: SelectListItem[]) {
                yield put(privateListActions.fetchItemStatusesSuccess(statuses));
            }
        );
    }
}

export function* watchItemStatuses() {
    yield takeLeading(PrivateListItemStatusActionType.FETCH_PRIVATE_ITEM_STATUSES_BEGIN, fetchStatusesAsync);
}