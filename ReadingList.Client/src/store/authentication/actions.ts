import { AuthenticationActionType } from './actionTypes';
import { ActionType, createAction } from 'typesafe-actions';
import { AuthenticationData, Credentials } from '../../models';

export const authenticationActions = {
    signInBegin: createAction(
        AuthenticationActionType.SIGN_IN_BEGIN,
        action => (credentials: Credentials) => action(credentials)
    ),
    signInSuccess: createAction(
        AuthenticationActionType.SIGN_IN_SUCCESS,
        action => (authData: AuthenticationData) => action(authData)
    ),
    signOut: createAction(AuthenticationActionType.SIGN_OUT)
};

export type AuthenticationAction = ActionType<typeof authenticationActions>;