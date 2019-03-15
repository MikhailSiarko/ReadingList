import { ModeratorActionType } from './actionTypes';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from 'src/store/saga';
import { takeLeading, put } from 'redux-saga/effects';
import { Action } from 'redux';
import { UsersService } from 'src/services';
import { sharedListActions } from '..';
import { User } from 'src/models';

function* fetchModeratorsAsync(action: Action) {
    if(isActionOf(sharedListActions.fetchModeratorsBegin, action)) {
        yield executeAsync(
            () => new UsersService().getUsers(),
            function* (users: User[]) {
                yield put(sharedListActions.fetchModeratorsSuccess(users));
            }
        );
    }
}

export function* watchModeratorsSaga() {
    yield takeLeading(ModeratorActionType.FETCH_MODERATORS_BEGIN, fetchModeratorsAsync);
}