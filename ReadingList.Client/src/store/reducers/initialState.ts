import { RouterState } from 'connected-react-router';

export default {
    router: { } as RouterState,
    identity: {
        isAuthenticated: sessionStorage.getItem('token') !== null,
        user: undefined
    },
    loading: false,
    privateList: null
};