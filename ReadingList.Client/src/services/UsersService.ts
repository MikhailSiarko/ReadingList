import ApiService from './ApiService';
import ApiConfiguration from '../config/ApiConfiguration';
import { onError } from '../utils';
import { User } from '../models';

export class UsersService extends ApiService {
    getUsers = () => {
        return this.configureRequest(ApiConfiguration.USERS, 'GET')
            .then(this.onSuccess<User[]>())
            .catch(onError);
    }
}