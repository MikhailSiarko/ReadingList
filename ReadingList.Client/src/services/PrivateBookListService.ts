import ApiConfiguration from '../config/ApiConfiguration';
import { PrivateBookList, PrivateBookListItem, RequestResult, SelectListItem } from '../models';
import ApiService from './ApiService';
import { onError } from '../utils';

export class PrivateBookListService extends ApiService {
    getList = () => {
        return this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'GET')
            .then(this.onSuccess<PrivateBookList>())
            .catch(onError);
    }

    getBookStatuses = () => {
        return this.configureRequest(ApiConfiguration.BOOK_STATUSES, 'GET')
            .then(this.onSuccess<SelectListItem[]>())
            .catch(onError);
    }

    addItem = (item: PrivateBookListItem) => {
        return this.configureRequest(`${ApiConfiguration.PRIVATE_LIST_ITEMS}`, 'POST',
            {
                title: item.title,
                author: item.author
            })
            .then(this.onSuccess<PrivateBookListItem>())
            .catch(onError);
    }

    updateItem = (item: PrivateBookListItem) => {
        return this.configureRequest(
            ApiConfiguration.getPrivateListItemUrl(item.id),
            'PUT', {
                status: item.status
            })
            .then(this.onSuccess<PrivateBookListItem>())
            .catch(onError);
    }

    removeItem = (id: number) => {
        return this.configureRequest(ApiConfiguration.getPrivateListItemUrl(id), 'DELETE')
            .then(this.onDeleteItemSuccess(id))
            .catch(onError);
    }

    updateList = (list: PrivateBookList) => {
        return this.configureRequest(`${ApiConfiguration.PRIVATE_LIST}`, 'PUT', {name: list.name})
            .then(this.onSuccess<PrivateBookList>())
            .catch(onError);
    }

    private onDeleteItemSuccess(id: number) {
        return () => new RequestResult<number>(true, id);
    }
}