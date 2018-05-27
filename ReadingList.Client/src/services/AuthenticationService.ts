import ApiConfiguration from '../config/ApiConfiguration';
import { AxiosResponse } from 'axios';
import { Credentials } from '../store/actions/authentication/infrastructure';
import { Dispatch } from 'redux';
import { RootState } from '../store/reducers';
import { RequestResult } from '../models/Request';
import { authenticationActions, AuthenticationData } from '../store/actions/authentication';
import { loadingActions } from '../store/actions/loading';
import ApiService from './ApiService';

export class AuthenticationService extends ApiService {
    login(dispatch: Dispatch<RootState>, credentials: Credentials) {
        const requestPromise = this.configureRequest(ApiConfiguration.LOGIN, 'POST', credentials);
        return requestPromise
            .then(this.onSuccess(dispatch))
            .catch(this.onError(dispatch));
    }

    register(dispatch: Dispatch<RootState>, credentials: Credentials) {
        const requestPromise = this.configureRequest(ApiConfiguration.REGISTER, 'POST', credentials);
        return requestPromise
            .then(this.onSuccess(dispatch))
            .catch(this.onError(dispatch));
    }

    private onSuccess(dispatch: Dispatch<RootState>) {
        return function(response: AxiosResponse) {
            const result = response.data as RequestResult<AuthenticationData>;
            dispatch(authenticationActions.signIn(result.data));
            dispatch(loadingActions.end());
            return result;
        };
    }

    private onError(dispatch: Dispatch<RootState>) {
        return function(error: AxiosResponse) {
            const result = error.data as RequestResult<never>;
            dispatch(loadingActions.end());
            return result;
        };
    }
}
