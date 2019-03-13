import { Action } from 'redux';
import { moderatedListActions } from './actions';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '..';
import { ListsService } from 'src/services';
import { takeLeading } from 'redux-saga/effects';
import { ModeratedListActionType } from './actionTypes';

function* fetchModeratedListsAsync(action: Action) {
    if(isActionOf(moderatedListActions.fetchBegin, action)) {
        yield executeAsync(
            () => new ListsService().getModeratedLists(),
            moderatedListActions.fetchSuccess,
            null,
            true
        );
    }
}

export function* watchModeratedListsSaga() {
    yield takeLeading(ModeratedListActionType.FETCH_MODERATED_LISTS_BEGIN, fetchModeratedListsAsync);
}