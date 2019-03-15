import ApiConfiguration from '../config/apiConfig';
import { Credentials, AuthenticationData } from '../models';
import ApiService from './api';
import { onError } from '../utils';

export class AuthenticationService extends ApiService {
    login = (credentials: Credentials) => {
        return this.configureRequest(ApiConfiguration.LOGIN, 'POST', credentials)
            .then(this.onSuccess<AuthenticationData>())
            .catch(onError);
    }

    register = (credentials: Credentials) => {
        return this.configureRequest(ApiConfiguration.REGISTER, 'POST', credentials)
            .then(this.onSuccess<AuthenticationData>())
            .catch(onError);
    }
}
