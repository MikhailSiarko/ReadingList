import { combineReducers } from 'redux';
import { RouterState } from 'connected-react-router';
import { routerReducer } from 'react-router-redux';
import { SelectListItem, PrivateBookList, User, Chunked, Book, ListInfo } from 'src/models';
import { NotificationType } from 'src/models/NotificationType';
import { loadingReducer } from './loading/reducer';
import { notificationReducer } from './notification';
import { authenticationReducer } from './authentication';
import { privateBookListReducer } from './privateList';
import { bookReducer } from './book';
import { moderatedListReducer } from './moderatedList';

export type RootState = Readonly<{
    loading: RootState.Loading;
    identity: RootState.Identity;
    router: RouterState;
    private: RootState.Private;
    notification: RootState.Notification,
    books: RootState.Books;
    moderatedLists: RootState.ModeratedLists
}>;

export namespace RootState {
    export type Loading = boolean;
    export type Authenticated = boolean;
    export type Notification = {
        hidden: boolean;
        message: String;
        type: NotificationType | undefined
    };
    export type Identity = {
        isAuthenticated: Authenticated;
        user: User | null;
    };
    export type Private = {
        list: PrivateBookList | null,
        bookStatuses: SelectListItem[] | null
    };
    export type Books = Chunked<Book> | null;
    export type ModeratedLists = ListInfo[] | null;
}

export const rootReducer = combineReducers<RootState>({
        loading: loadingReducer,
        notification: notificationReducer,
        identity: authenticationReducer,
        private: privateBookListReducer,
        books: bookReducer,
        moderatedLists: moderatedListReducer,
        router: routerReducer
    }
);