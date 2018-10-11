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
        return this.configureRequest(ApiConfiguration.LOGIN, 'POST', credentials)
            .then(this.onSuccess)
            .catch(onError);
    }

    register(credentials: Credentials) {
        return this.configureRequest(ApiConfiguration.REGISTER, 'POST', credentials)
            .then(this.onSuccess)
            .catch(onError);
    }

    private onSuccess = (response: AxiosResponse) => {
        const result = new RequestResult<AuthenticationData>(true, response.data);
        this.dispatch(authenticationActions.signIn(result.data as AuthenticationData));
        return result;
    }
}
