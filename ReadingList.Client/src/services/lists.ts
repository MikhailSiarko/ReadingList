import ApiService from './api';
import ApiConfiguration from '../config/apiConfig';
import { onError } from '../utils';
import { ListInfo } from '../models';

export class ListsService extends ApiService {
    getModeratedLists = () => {
        return this.configureRequest(ApiConfiguration.MODERATED_LISTS, 'GET')
            .then(this.onSuccess<ListInfo[]>())
            .catch(onError);
    }
}