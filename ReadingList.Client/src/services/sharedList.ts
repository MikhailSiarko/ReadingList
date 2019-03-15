import ApiService from './api';
import { ApiConfiguration } from '../config/apiConfig';
import { onError } from '../utils';
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
        return this.configureRequest(ApiConfiguration.getMySharedListsUrl(chunk, count), 'GET')
            .then(this.onSuccess<Chunked<SharedBookListPreview>>())
            .catch(onError);
    }

    getLists = (query: string, chunk: number | null, count: number | null) => {
        return this.configureRequest(ApiConfiguration.getFindSharedListsUrl(query, chunk, count), 'GET')
            .then(this.onSuccess<Chunked<SharedBookListPreview>>())
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

    createList = (data: SharedListCreateData) => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS, 'POST', data)
            .then(this.onSuccess<SharedBookListPreview>())
            .catch(onError);
    }

    updateList = (id: number, data: SharedListUpdateData) => {
        return this.configureRequest(
            ApiConfiguration.SHARED_LISTS + `/${id}`, 'PATCH',
            data
        )
        .then(this.onSuccess<SharedBookList>())
        .catch(onError);
    }

    deleteItem = (listId: number, itemId: number) => {
        return this.configureRequest(ApiConfiguration.getSharedListItemUrl(listId, itemId), 'DELETE')
            .then(this.onDeleteItemSuccess(itemId))
            .catch(onError);
    }

    deleteList = (listId: number) => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS + `/${listId}`, 'DELETE')
            .then(this.onSuccess<never>())
            .catch(onError);
    }

    private onDeleteItemSuccess(id: number) {
        return () => new RequestResult<number>(true, id);
    }
}