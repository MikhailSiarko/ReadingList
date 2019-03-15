import { ModeratorActionType } from './actionTypes';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '../../saga';
import { takeLeading, put } from 'redux-saga/effects';
import { Action } from 'redux';
import { UsersService } from '../../../services';
import { sharedListActions } from '..';
import { User } from '../../../models';

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