import { combineReducers } from 'redux';
import { RouterState } from 'connected-react-router';
import { routerReducer } from 'react-router-redux';
import {
    SelectListItem,
    PrivateBookList,
    User,
    Chunked,
    Book,
    ListInfo,
    SharedBookList,
    Tag,
    NotificationType,
    SharedBookListPreview
} from 'src/models';
import { loadingReducer } from './loading/reducer';
import { notificationReducer } from './notification';
import { authenticationReducer } from './authentication';
import { privateBookListReducer } from './privateList';
import { bookReducer } from './books';
import { moderatedListReducer } from './moderatedList';
import { sharedBookListReducer } from './sharedList';
import { tagReducer } from './tags';

export type RootState = Readonly<{
    loading: RootState.Loading;
    identity: RootState.Identity;
    router: RouterState;
    private: RootState.Private;
    shared: RootState.Shared;
    notification: RootState.Notification,
    books: RootState.Books;
    moderatedLists: RootState.ModeratedLists
    tags: RootState.Tags
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
    export type Shared = {
        lists: Chunked<SharedBookListPreview> | null;
        list: SharedBookList | null;
        moderators: User[] | null
    };
    export type Tags = Tag[] | null;
}

export const rootReducer = combineReducers<RootState>({
        loading: loadingReducer,
        notification: notificationReducer,
        identity: authenticationReducer,
        shared: sharedBookListReducer,
        private: privateBookListReducer,
        books: bookReducer,
        moderatedLists: moderatedListReducer,
        tags: tagReducer,
        router: routerReducer
    }
);