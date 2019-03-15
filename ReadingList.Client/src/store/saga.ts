import { all, put, call, SimpleEffect } from 'redux-saga/effects';
import { RequestResult } from 'src/models';
import { watchModeratedListsSaga } from './moderatedList';
import { watchSignIn } from './authentication';
import { watchNotificationsSaga, notificationActions } from './notification';
import { rootPrivateListSaga } from './privateList';
import { watchBooksSaga } from './books';
import { loadingActions } from './loading';
import { rootSharedListSaga } from './sharedList/sagas';
import { watchTagsSaga } from './tags';

export function* rootSaga() {
    yield all([
        watchSignIn(),
        watchNotificationsSaga(),
        rootPrivateListSaga(),
        watchBooksSaga(),
        watchModeratedListsSaga(),
        rootSharedListSaga(),
        watchTagsSaga()
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

export function* execute<TEffectType, TPayload, TEffect extends SimpleEffect<TEffectType, TPayload>>(
    effects: (() => IterableIterator<TEffect>) | (() => void),
    loading?: boolean) {
        try {
            if(loading) {
                yield put(loadingActions.start());
            }
            yield effects();
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

export function* executeAsync<TEffectType, TPayload, TEffect extends SimpleEffect<TEffectType, TPayload>, TData>(
    asyncCall: () => Promise<RequestResult<TData>>,
    effects: ((data: TData) => IterableIterator<TEffect>) | null,
    loading?: boolean) {
        try {
            if(loading) {
                yield put(loadingActions.start());
            }
            const result: RequestResult<TData | undefined> = yield call(asyncCall);
            if(!result.isSucceed) {
                throw new RequestResultException(result);
            }
            if(effects) {
                yield effects(result.data as TData);
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