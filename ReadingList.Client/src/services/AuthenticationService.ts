import ApiConfiguration from '../config/ApiConfiguration';
import { Credentials, AuthenticationData } from '../models';
import ApiService from './ApiService';
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
