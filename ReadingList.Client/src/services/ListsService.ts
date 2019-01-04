import ApiService from './ApiService';
import ApiConfiguration from '../config/ApiConfiguration';
import { onError } from '../utils';
import { ListInfo } from '../models';

export class ListsService extends ApiService {
    getModeratedLists = () => {
        return this.configureRequest(ApiConfiguration.MODERATED_LISTS, 'GET')
            .then(this.onSuccess<ListInfo[]>())
            .catch(onError);
    }
}