import ApiService from './ApiService';
import { ApiConfiguration } from '../config/ApiConfiguration';
import { onError } from '../utils';
import { SharedBookList } from '../models/BookList/Implementations/SharedBookList';

export class SharedBookListService extends ApiService {
    getOwnLists = () => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS_OWN, 'GET')
            .then(this.onSuccess<SharedBookList[]>())
            .catch(onError);
    }

    getLists = (query: string) => {
        return this.configureRequest(ApiConfiguration.getFindSharedListsUrl(query), 'GET')
            .then(this.onSuccess<SharedBookList[]>())
            .catch(onError);
    }

    getList = (id: number) => {
        return this.configureRequest(ApiConfiguration.getSharedListUrl(id), 'GET')
            .then(this.onSuccess<SharedBookList>())
            .catch(onError);
    }
}