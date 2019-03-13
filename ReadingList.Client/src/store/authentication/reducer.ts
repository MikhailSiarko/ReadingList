import { authenticationActions, AuthenticationAction } from './actions';
import { getType } from 'typesafe-actions';
import { cloneDeep } from 'lodash';
import { RootState } from '../state';
import { initialState } from '../initialState';

export function authenticationReducer(state: RootState.Identity = initialState.identity,
                                      action: AuthenticationAction) {
    switch (action.type) {
        case getType(authenticationActions.signInSuccess):
            return {
                isAuthenticated: true,
                user: cloneDeep(action.payload)
            };
        case getType(authenticationActions.signOut):
            return {
                isAuthenticated: false,
                user: null
            };
        default:
            return state;
    }
}