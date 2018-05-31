import { combineReducers } from 'redux';
import { RouterState } from 'connected-react-router';
import { routerReducer } from 'react-router-redux';
import { authenticationReducer } from './authentication/authenticationReducer';
import { PrivateBookListModel, UserModel } from '../../models';
import { privateBookListReducer } from './privateBookList/privateBookListReducer';
import { loadingReducer } from './loading/loadingReducer';

export type RootState = Readonly<{
    loading: RootState.LoadingState;
    identity: RootState.IdentityState;
    router: RouterState;
    privateList: RootState.PrivateList;
}>;

export namespace RootState {
    export type LoadingState = boolean;
    export type UserState = UserModel | null;
    export type AuthenticatedState = boolean;
    export type IdentityState = {
        isAuthenticated: AuthenticatedState;
        user: UserState;
    };
    export type PrivateList = PrivateBookListModel | null;
}

export const rootReducer = combineReducers<RootState>({
        loading: loadingReducer,
        identity: authenticationReducer,
        privateList: privateBookListReducer,
        router: routerReducer
    }
);