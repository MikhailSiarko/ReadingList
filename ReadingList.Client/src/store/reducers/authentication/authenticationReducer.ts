import { RootState } from '../index';
import initialState from '../initialState';
import { AuthenticationAction, authenticationActions } from '../../actions/authentication';
import { getType } from 'typesafe-actions';
import { cloneDeep } from 'lodash';

export function authenticationReducer(state: RootState.IdentityState = initialState.identity,
                                      action: AuthenticationAction) {
    switch (action.type) {
        case getType(authenticationActions.signIn):
            sessionStorage.setItem('token', action.authData.token);
            return Object.assign({}, state, {
                isAuthenticated: true,
                user: cloneDeep(action.authData.user)
            } as RootState.IdentityState);
        case getType(authenticationActions.signOut):
            sessionStorage.removeItem('token');
            return Object.assign({}, state, {
                isAuthenticated: false,
                user: undefined
            } as RootState.IdentityState);
        default:
            return state;
    }
}