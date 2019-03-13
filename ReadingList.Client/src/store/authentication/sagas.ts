import { AuthenticationService } from 'src/services';
import { takeLeading, put } from 'redux-saga/effects';
import { Credentials, RequestResult, AuthenticationData } from 'src/models';
import { isActionOf } from 'typesafe-actions';
import { authenticationActions } from './actions';
import { executeAsync, execute } from '../saga';
import { push } from 'react-router-redux';
import { privateListActions } from '../privateList';
import { AuthenticationActionType } from './actionTypes';
import { Action } from 'redux';

function send(credentials: Credentials) {
    const authService = new AuthenticationService();
    if(credentials.confirmPassword) {
        return authService.register(credentials);
    }
    return authService.login(credentials);
}

function* signInRequestAsync(action: Action) {
    if(isActionOf(authenticationActions.signInBegin, action)) {
        yield executeAsync(
            () => send(action.payload),
            authenticationActions.signInSuccess,
            function* (result: RequestResult<any>) {
                setSessionData(result.data);
                yield put(push('/'));
            },
            true
        );
    }
}

function* signOut() {
    yield execute(unsetSessionData, true);
    yield put(privateListActions.unsetPrivate());
}

function setSessionData(data: AuthenticationData) {
    sessionStorage.setItem('reading_list', data.token);
    sessionStorage.setItem('reading_list_user', JSON.stringify(data.user));
}

function unsetSessionData() {
    sessionStorage.removeItem('reading_list');
    sessionStorage.removeItem('reading_list_user');
}

export function* watchSignIn() {
    yield takeLeading(AuthenticationActionType.SIGN_IN_BEGIN, signInRequestAsync);
    yield takeLeading(AuthenticationActionType.SIGN_OUT, signOut);
}