import ApiService from './ApiService';
import { ApiConfiguration } from '../config/ApiConfiguration';
import { onError } from '../utils';
import { SharedBookList, SharedBookListItem, SelectListItem } from '../models/BookList';

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

    addItem = (listId: number, bookId: number) => {
        return this.configureRequest(ApiConfiguration.getAddItemToSharedListUrl(listId), 'POST', {bookId})
            .then(this.onSuccess<SharedBookListItem>())
            .catch(onError);
    }

    createList = (data: { name: string, tags: SelectListItem[] }) => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS, 'POST', {name: data.name, tags: data.tags})
            .then(this.onSuccess<SharedBookList>())
            .catch(onError);
    }
}