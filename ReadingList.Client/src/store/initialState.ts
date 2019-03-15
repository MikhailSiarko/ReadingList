import { RouterState } from 'connected-react-router';
import { isNullOrEmpty } from 'src/utils';
import { RootState } from './state';

export const initialState: RootState = {
    router: {} as RouterState,
    identity: {
        isAuthenticated: !isNullOrEmpty(sessionStorage.getItem('reading_list'))
            && !isNullOrEmpty(sessionStorage.getItem('reading_list_user')),
        user: JSON.parse(sessionStorage.getItem('reading_list_user') as string)
    },
    loading: false,
    private: {
        list: null,
        bookStatuses: null
    },
    notification: {
        hidden: true,
        message: '',
        type: undefined
    },
    books: null,
    moderatedLists: null,
    shared: {
        lists: null,
        list: null,
        moderators: null
    },
    tags: null
};