import { Action } from 'redux';
import { moderatedListActions } from './actions';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '..';
import { ListsService } from '../../services';
import { takeLeading, put } from 'redux-saga/effects';
import { ModeratedListActionType } from './actionTypes';
import { ListInfo } from '../../models';

function* fetchModeratedListsAsync(action: Action) {
    if(isActionOf(moderatedListActions.fetchBegin, action)) {
        yield executeAsync(
            () => new ListsService().getModeratedLists(),
            function* (lists: ListInfo[]) {
                yield put(moderatedListActions.fetchSuccess(lists));
            },
            true
        );
    }
}

export function* watchModeratedListsSaga() {
    yield takeLeading(ModeratedListActionType.FETCH_MODERATED_LISTS_BEGIN, fetchModeratedListsAsync);
}