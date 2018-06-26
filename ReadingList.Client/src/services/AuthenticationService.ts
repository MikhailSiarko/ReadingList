import ApiConfiguration from '../config/ApiConfiguration';
import { AxiosResponse } from 'axios';
import { Credentials } from '../store/actions/authentication/infrastructure';
import { Dispatch } from 'redux';
import { RootState } from '../store/reducers';
import { RequestResult } from '../models';
import { authenticationActions, AuthenticationData } from '../store/actions/authentication';
import ApiService from './ApiService';
import { onError } from '../utils';

export class AuthenticationService extends ApiService {
    constructor(dispatch: Dispatch<RootState>) {
        super(dispatch);
    }
    login(credentials: Credentials) {
        const requestPromise = this.configureRequest(ApiConfiguration.LOGIN, 'POST', credentials);
        return requestPromise
            .then(this.onSuccess())
            .catch(onError);
    }

    register(credentials: Credentials) {
        const requestPromise = this.configureRequest(ApiConfiguration.REGISTER, 'POST', credentials);
        return requestPromise
            .then(this.onSuccess())
            .catch(onError);
    }

    private onSuccess() {
        const dispatch = this.dispatch;
        return function(response: AxiosResponse) {
            const result = new RequestResult<AuthenticationData>(true, response.data);
            dispatch(authenticationActions.signIn(result.data as AuthenticationData));
            return result;
        };
    }
}
