import { all, put, call } from 'redux-saga/effects';
import { RequestResult } from 'src/models';
import { watchModeratedListsSaga } from './moderatedList';
import { watchSignIn } from './authentication';
import { watchNotificationsSaga, notificationActions } from './notification';
import { rootPrivateListSaga } from './privateList';
import { watchBooksSaga } from './book';
import { loadingActions } from './loading';

export function* rootSaga() {
    yield all([
        watchSignIn(),
        watchNotificationsSaga(),
        rootPrivateListSaga(),
        watchBooksSaga(),
        watchModeratedListsSaga()
    ]);
}

class RequestResultException {
    message: string;
    status: number;
    constructor(message: string, status: number) {
        this.message = message;
        this.status = status;
    }
}

export function* executeAsync(
    asyncSideEffect: () => Promise<RequestResult<any>>,
    onSuccessAction?: ((data: any) => any) | null,
    afterSuccessAction?: ((result: RequestResult<any>) => IterableIterator<any>) | null,
    loading: boolean = false) {
        try {
            if(loading) {
                yield put(loadingActions.start());
            }
            const result = yield call(asyncSideEffect);
            if(result.isSucceed) {
                if(onSuccessAction) {
                    yield put(onSuccessAction(result.data));
                }
                if(afterSuccessAction) {
                    yield afterSuccessAction(result);
                }
            } else {
                throw new RequestResultException(result.errorMessage, result.status);
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