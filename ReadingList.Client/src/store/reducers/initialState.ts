import { RouterState } from 'connected-react-router';
import { isNullOrEmpty } from '../../utils';

export default {
    router: { } as RouterState,
    identity: {
        isAuthenticated: !isNullOrEmpty(sessionStorage.getItem('reading_list'))
            && !isNullOrEmpty(sessionStorage.getItem('reading_list_user')),
        user: JSON.parse(sessionStorage.getItem('reading_list_user') as string)
    },
    loading: false,
    privateList: null
};