import { combineReducers } from 'redux';
import { RouterState } from 'connected-react-router';
import { routerReducer } from 'react-router-redux';
import { authenticationReducer } from './authentication/authenticationReducer';
import { requestReducer } from './request/requestReducer';

export type RootState = Readonly<{
    loading: RootState.LoadingState;
    identity: RootState.IdentityState;
    router: RouterState;
}>;

export namespace RootState {
    export type LoadingState = boolean;
    export type UserState = {
        email: string;
        firstname: string;
        lastname: string;
    };
    export type AuthenticatedState = boolean;
    export type IdentityState = {
        isAuthenticated: AuthenticatedState;
        user: UserState | undefined;
    };
}

export const rootReducer = combineReducers<RootState>({
        request: requestReducer,
        identity: authenticationReducer,
        router: routerReducer
    }
);