import { RootState } from '../store/reducers';
import { Dispatch } from 'redux';
import ApiConfiguration from '../config/ApiConfiguration';
import { AxiosResponse } from 'axios';
import { privateBookListAction } from '../store/actions/privateBookList';
import { PrivateBookList, PrivateBookListItem, RequestResult, SelectListItem } from '../models';
import ApiService from './ApiService';
import { onError } from '../utils';

export class PrivateBookListService extends ApiService {
    constructor(dispatch: Dispatch<RootState>) {
        super(dispatch);
    }

    getList() {
        return this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'GET')
            .then(this.onGetListSuccess)
            .catch(onError);
    }

    getBookStatuses() {
        return this.configureRequest(ApiConfiguration.BOOK_STATUSES, 'GET')
            .then(this.onGetBookStatusesSuccess)
            .catch(onError);
    }

    addItem(item: PrivateBookListItem) {
        return this.configureRequest(`${ApiConfiguration.PRIVATE_LIST_ITEMS}`, 'POST',
            {
                title: item.title,
                author: item.author
            })
            .then(this.onAddItemSuccess)
            .catch(onError);
    }

    updateItem(item: PrivateBookListItem) {
        return this.configureRequest(
            ApiConfiguration.getPrivateListItemUrl(item.id),
            'PUT', {
                title: item.title,
                author: item.author,
                status: item.status
            })
            .then(this.onUpdateItemSuccess)
            .catch(onError);
    }

    removeItem(id: number) {
        return this.configureRequest(ApiConfiguration.getPrivateListItemUrl(id), 'DELETE')
            .then(this.onDeleteItemSuccess(id))
            .catch(onError);
    }

    updateList(list: PrivateBookList) {
        return this.configureRequest(`${ApiConfiguration.PRIVATE_LIST}`, 'PUT', {name: list.name})
            .then(this.onListUpdate)
            .catch(onError);
    }

    private onListUpdate = (response: AxiosResponse) => {
        const result = new RequestResult<PrivateBookList>(true, response.data);
        this.dispatch(privateBookListAction.updateList(result.data as PrivateBookList));
        return result;
    }

    private onGetListSuccess = (response: AxiosResponse) => {
        const result = new RequestResult<PrivateBookList>(true, response.data);
        this.dispatch(privateBookListAction.updateList(result.data as PrivateBookList));
        return result;
    }

    private onAddItemSuccess = (response: AxiosResponse) => {
        const result = new RequestResult<PrivateBookListItem>(true, response.data);
        this.dispatch(privateBookListAction.addItem(result.data as PrivateBookListItem));
        return result;
    }

    private onUpdateItemSuccess = (response: AxiosResponse) => {
        const result = new RequestResult<PrivateBookListItem>(true, response.data);
        this.dispatch(privateBookListAction.updateItem(result.data as PrivateBookListItem));
        return result;
    }

    private onDeleteItemSuccess(id: number) {
        return () => {
            this.dispatch(privateBookListAction.removeItem(id));
        };
    }

    private onGetBookStatusesSuccess = (response: AxiosResponse) => {
        const result = new RequestResult<SelectListItem[]>(true, response.data);
        this.dispatch(privateBookListAction.setBookStatuses(result.data as SelectListItem[]));
        return result;
    }
}