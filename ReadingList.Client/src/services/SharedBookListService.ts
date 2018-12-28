import ApiService from './ApiService';
import { ApiConfiguration } from '../config/ApiConfiguration';
import { onError } from '../utils';
import { SharedBookList, SharedBookListItem, SharedBookListPreview } from '../models/BookList';
import { Tag } from '../models/Tag';
import { RequestResult } from '../models';

export class SharedBookListService extends ApiService {
    getOwnLists = () => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS_OWN, 'GET')
            .then(this.onSuccess<SharedBookListPreview[]>())
            .catch(onError);
    }

    getLists = (query: string) => {
        return this.configureRequest(ApiConfiguration.getFindSharedListsUrl(query), 'GET')
            .then(this.onSuccess<SharedBookListPreview[]>())
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

    createList = (data: { name: string, tags: Tag[] }) => {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS, 'POST', {name: data.name, tags: data.tags})
            .then(this.onSuccess<SharedBookListPreview>())
            .catch(onError);
    }

    updateList = (data: { id: number, name: string, tags: Tag[], moderators: number[] | null }) => {
        return this.configureRequest(
            ApiConfiguration.SHARED_LISTS + `/${data.id}`, 'PUT',
            {
                name: data.name,
                tags: data.tags,
                moderators: data.moderators
            }
        )
        .then(this.onSuccess<SharedBookList>())
        .catch(onError);
    }

    removeItem = (listId: number, itemId: number) => {
        return this.configureRequest(ApiConfiguration.getSharedListItemUrl(listId, itemId), 'DELETE')
            .then(this.onDeleteItemSuccess(itemId))
            .catch(onError);
    }

    private onDeleteItemSuccess(id: number) {
        return () => new RequestResult<number>(true, id);
    }
}