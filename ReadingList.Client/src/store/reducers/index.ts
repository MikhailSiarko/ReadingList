import { combineReducers } from 'redux';
import { RouterState } from 'connected-react-router';
import { routerReducer } from 'react-router-redux';
import { authenticationReducer } from './authentication/authenticationReducer';
import { PrivateBookList, UserModel, SelectListItem } from '../../models';
import { privateBookListReducer } from './privateBookList/privateBookListReducer';
import { loadingReducer } from './loading/loadingReducer';

export type RootState = Readonly<{
    loading: RootState.LoadingState;
    identity: RootState.IdentityState;
    router: RouterState;
    private: RootState.Private;
}>;

export namespace RootState {
    export type LoadingState = boolean;
    export type UserState = UserModel | null;
    export type AuthenticatedState = boolean;
    export type IdentityState = {
        isAuthenticated: AuthenticatedState;
        user: UserState;
    };
    export type Private = {
        list: PrivateBookList | null,
        bookStatuses: SelectListItem[] | null
    };
}

export const rootReducer = combineReducers<RootState>({
        loading: loadingReducer,
        identity: authenticationReducer,
        private: privateBookListReducer,
        router: routerReducer
    }
);