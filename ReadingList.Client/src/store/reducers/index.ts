import { combineReducers } from 'redux';
import { RouterState } from 'connected-react-router';
import { routerReducer } from 'react-router-redux';
import { authenticationReducer } from './authentication/authenticationReducer';
import { PrivateBookList, UserModel, SelectListItem } from '../../models';
import { privateBookListReducer } from './privateBookList/privateBookListReducer';
import { loadingReducer } from './loading/loadingReducer';
import { notificationReducer } from './notification/notificationReducer';
import { NotificationType } from '../../models/NotificationType';

export type RootState = Readonly<{
    loading: RootState.Loading;
    identity: RootState.Identity;
    router: RouterState;
    private: RootState.Private;
    notification: RootState.Notification
}>;

export namespace RootState {
    export type Loading = boolean;
    export type User = UserModel | null;
    export type Authenticated = boolean;
    export type Notification = {
        hidden: boolean;
        message: String;
        type: NotificationType | undefined
    };
    export type Identity = {
        isAuthenticated: Authenticated;
        user: User;
    };
    export type Private = {
        list: PrivateBookList | null,
        bookStatuses: SelectListItem[] | null
    };
}

export const rootReducer = combineReducers<RootState>({
        loading: loadingReducer,
        notification: notificationReducer,
        identity: authenticationReducer,
        private: privateBookListReducer,
        router: routerReducer
    }
);