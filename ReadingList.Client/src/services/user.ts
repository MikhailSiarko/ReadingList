import ApiService from './api';
import ApiConfiguration from '../config/apiConfig';
import { onError } from '../utils';
import { User } from '../models';

export class UsersService extends ApiService {
    getUsers = () => {
        return this.configureRequest(ApiConfiguration.USERS, 'GET')
            .then(this.onSuccess<User[]>())
            .catch(onError);
    }
}