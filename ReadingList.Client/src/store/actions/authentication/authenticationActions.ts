import { AuthenticationActionType } from './AuthenticationActionType';
import { createAction, getReturnOfExpression } from 'typesafe-actions';
import { AuthenticationData } from '../../../models';

export const authenticationActions = {
    signIn: createAction(AuthenticationActionType.SIGN_IN, (authData: AuthenticationData) => {
        return {
            type: AuthenticationActionType.SIGN_IN,
            authData
        };
    }),
    signOut: createAction(AuthenticationActionType.SIGN_OUT)
};

const returnOfActions = Object.values(authenticationActions).map(getReturnOfExpression);
export type AuthenticationAction = typeof returnOfActions[number];