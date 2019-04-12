import ApiConfiguration from '../config/apiConfig';
import {
    PrivateBookList,
    PrivateBookListItem,
    RequestResult,
    SelectListItem,
    PrivateListUpdateData,
    PrivateItemUpdateData
} from '../models';
import ApiService from './api';

export class PrivateListService extends ApiService {
    getList = () => {
        return this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'GET')
            .then(this.onSuccess<PrivateBookList>())
            .catch(this.onError);
    }

    getItemStatuses = () => {
        return this.configureRequest(ApiConfiguration.BOOK_STATUSES, 'GET')
            .then(this.onSuccess<SelectListItem[]>())
            .catch(this.onError);
    }

    addItem = (bookId: number) => {
        return this.configureRequest(
            ApiConfiguration.PRIVATE_LIST_ITEMS, 'POST', { bookId })
            .then(this.onSuccess<PrivateBookListItem>())
            .catch(this.onError);
    }

    updateItem = (itemId: number, data: PrivateItemUpdateData) => {
        return this.configureRequest(
            ApiConfiguration.getPrivateListItemUrl(itemId),
            'PATCH',
            data
        )
        .then(this.onSuccess<PrivateBookListItem>())
        .catch(this.onError);
    }

    deleteItem = (id: number) => {
        return this.configureRequest(ApiConfiguration.getPrivateListItemUrl(id), 'DELETE')
            .then(this.onDeleteItemSuccess(id))
            .catch(this.onError);
    }

    updateList = (data: PrivateListUpdateData) => {
        return this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'PATCH', data)
            .then(this.onSuccess<PrivateBookList>())
            .catch(this.onError);
    }

    sharePrivateList = (name: string) => {
        return this.configureRequest(ApiConfiguration.getSharePrivateListUrl(name), 'POST')
            .then(this.onSuccess<never>())
            .catch(this.onError);
    }

    private onDeleteItemSuccess(id: number) {
        return () => new RequestResult<number>(true, id);
    }
}