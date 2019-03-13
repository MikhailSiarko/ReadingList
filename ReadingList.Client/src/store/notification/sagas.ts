import { put, delay, takeLeading } from 'redux-saga/effects';
import { notificationActions } from './actions';
import { NotificationActionType } from './actionTypes';

function* notificationAsync() {
    yield delay(3000);
    yield put(notificationActions.hide());
}

export function* watchNotificationsSaga() {
    yield takeLeading(NotificationActionType.ERROR, notificationAsync);
    yield takeLeading(NotificationActionType.INFO, notificationAsync);
}