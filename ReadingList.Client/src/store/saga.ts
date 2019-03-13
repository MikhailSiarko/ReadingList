import { all, put, call } from 'redux-saga/effects';
import { RequestResult } from 'src/models';
import { watchModeratedListsSaga } from './moderatedList';
import { watchSignIn } from './authentication';
import { watchNotificationsSaga, notificationActions } from './notification';
import { rootPrivateListSaga } from './privateList';
import { watchBooksSaga } from './book';
import { loadingActions } from './loading';
import { Action } from 'redux';

export function* rootSaga() {
    yield all([
        watchSignIn(),
        watchNotificationsSaga(),
        rootPrivateListSaga(),
        watchBooksSaga(),
        watchModeratedListsSaga()
    ]);
}

class RequestResultException<TData> {
    message: string;
    status: number;
    constructor(result: RequestResult<TData>) {
        if(result.errorMessage) {
            this.message = result.errorMessage;
        }

        if(result.status) {
            this.status = result.status;
        }
    }
}

export function* executeAsync<TData, TAction extends Action & { payload: TData }, TAfterSuccessResult>(
    asyncSideEffect: () => Promise<RequestResult<TData>>,
    onSuccessAction?: ((data: TData) => TAction) | null,
    afterSuccess?: ((data: TData) => TAfterSuccessResult) | null,
    loading: boolean = false) {
        try {
            if(loading) {
                yield put(loadingActions.start());
            }
            const result: RequestResult<TData> = yield call(asyncSideEffect);
            if(result.isSucceed && result.data) {
                if(onSuccessAction) {
                    yield put(onSuccessAction(result.data));
                }
                if(afterSuccess) {
                    yield afterSuccess(result.data);
                }
            } else if(!result.isSucceed) {
                throw new RequestResultException(result);
            }
        } catch (error) {
            yield put(
                error.status && error.status < 500
                    ? notificationActions.info(error.message as string)
                    : notificationActions.error(error.message as string)
            );
        } finally {
            if(loading) {
                yield put(loadingActions.end());
            }
        }
}

export function* execute(sideEffect: () => void, loading?: boolean) {
    try {
        if(loading) {
            yield put(loadingActions.start());
        }
        sideEffect();
    } catch (error) {
        yield put(notificationActions.error(error.response
            ? error.response.data ? error.response.data.errorMessage : error.message
            : error.message));
    } finally {
        if(loading) {
            yield put(loadingActions.end());
        }
    }
}