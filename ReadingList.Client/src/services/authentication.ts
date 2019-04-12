import ApiConfiguration from '../config/apiConfig';
import { Credentials, AuthenticationData } from '../models';
import ApiService from './api';

export class AuthenticationService extends ApiService {
    login = (credentials: Credentials) => {
        return this.configureRequest(ApiConfiguration.LOGIN, 'POST', credentials)
            .then(this.onSuccess<AuthenticationData>())
            .catch(this.onError);
    }

    register = (credentials: Credentials) => {
        return this.configureRequest(ApiConfiguration.REGISTER, 'POST', credentials)
            .then(this.onSuccess<AuthenticationData>())
            .catch(this.onError);
    }
}
