import ApiConfiguration from '../config/ApiConfiguration';
import { PrivateBookList, PrivateBookListItem, RequestResult, SelectListItem } from '../models';
import ApiService from './ApiService';
import { onError } from '../utils';

export class PrivateListService extends ApiService {
    getList = () => {
        return this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'GET')
            .then(this.onSuccess<PrivateBookList>())
            .catch(onError);
    }

    getItemStatuses = () => {
        return this.configureRequest(ApiConfiguration.BOOK_STATUSES, 'GET')
            .then(this.onSuccess<SelectListItem[]>())
            .catch(onError);
    }

    addItem = (bookId: number) => {
        return this.configureRequest(`${ApiConfiguration.PRIVATE_LIST_ITEMS}`, 'POST',
            {
                bookId: bookId
            })
            .then(this.onSuccess<PrivateBookListItem>())
            .catch(onError);
    }

    updateItem = (item: PrivateBookListItem) => {
        return this.configureRequest(
            ApiConfiguration.getPrivateListItemUrl(item.id),
            'PATCH',
            {
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
        return this.configureRequest(`${ApiConfiguration.PRIVATE_LIST}`, 'PATCH', {name: list.name})
            .then(this.onSuccess<PrivateBookList>())
            .catch(onError);
    }

    sharePrivateList = (name: string) => {
        return this.configureRequest(ApiConfiguration.getSharePrivateListUrl(name), 'POST')
            .then(this.onSuccess<never>())
            .catch(onError);
    }

    private onDeleteItemSuccess(id: number) {
        return () => new RequestResult<number>(true, id);
    }
}