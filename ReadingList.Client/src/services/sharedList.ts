import ApiService from './api';
import { ApiConfiguration } from '../config/apiConfig';
import {
    RequestResult,
    Chunked,
    SharedBookList,
    SharedBookListItem,
    SharedBookListPreview,
    SharedListUpdateData,
    SharedListCreateData
} from '../models';

export class SharedListService extends ApiService {
    getMyLists = (chunk: number | null, count: number | null) => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS_MY, 'GET', undefined, { chunk, count })
            .then(this.onSuccess<Chunked<SharedBookListPreview>>())
            .catch(this.onError);
    }

    getLists = (query: string, chunk: number | null, count: number | null) => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS, 'GET', undefined, { query, chunk, count })
            .then(this.onSuccess<Chunked<SharedBookListPreview>>())
            .catch(this.onError);
    }

    getList = (id: number) => {
        return this.configureRequest(ApiConfiguration.getSharedListUrl(id), 'GET')
            .then(this.onSuccess<SharedBookList>())
            .catch(this.onError);
    }

    addItem = (listId: number, bookId: number) => {
        return this.configureRequest(ApiConfiguration.getAddItemToSharedListUrl(listId), 'POST', {bookId})
            .then(this.onSuccess<SharedBookListItem>())
            .catch(this.onError);
    }

    createList = (data: SharedListCreateData) => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS, 'POST', data)
            .then(this.onSuccess<SharedBookListPreview>())
            .catch(this.onError);
    }

    updateList = (id: number, data: SharedListUpdateData) => {
        return this.configureRequest(
            ApiConfiguration.getSharedListUrl(id), 'PATCH',
            data
        )
        .then(this.onSuccess<SharedBookList>())
        .catch(this.onError);
    }

    deleteItem = (listId: number, itemId: number) => {
        return this.configureRequest(ApiConfiguration.getSharedListItemUrl(listId, itemId), 'DELETE')
            .then(this.onDeleteItemSuccess(itemId))
            .catch(this.onError);
    }

    deleteList = (listId: number) => {
        return this.configureRequest(ApiConfiguration.getSharedListUrl(listId), 'DELETE')
            .then(this.onSuccess<never>())
            .catch(this.onError);
    }

    private onDeleteItemSuccess(id: number) {
        return () => new RequestResult<number>(true, id);
    }
}