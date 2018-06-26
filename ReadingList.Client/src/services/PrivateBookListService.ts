import { RootState } from '../store/reducers';
import { Dispatch } from 'redux';
import ApiConfiguration from '../config/ApiConfiguration';
import { AxiosResponse } from 'axios';
import { privateBookListAction } from '../store/actions/privateBookList';
import { PrivateBookListModel, PrivateBookListItemModel, RequestResult } from '../models';
import ApiService from './ApiService';
import { onError } from '../utils';

export class PrivateBookListService extends ApiService {
    constructor(dispatch: Dispatch<RootState>) {
        super(dispatch);
    }
    public getList() {
        const requestPromise = this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'GET');
        return requestPromise
            .then(this.onGetListSuccess())
            .catch(onError);
    }

    public addItem(item: PrivateBookListItemModel) {
        const requestPromise = this.configureRequest(`${ApiConfiguration.PRIVATE_LIST_ITEMS}`,
            'POST', {title: item.title, author: item.author});
        return requestPromise
            .then(this.onAddItemSuccess())
            .catch(onError);
    }

    public updateItem(item: PrivateBookListItemModel) {
        const requestPromise = this.configureRequest(
            `${ApiConfiguration.PRIVATE_LIST_ITEMS}/${item.id}`,
            'PUT', {
                title: item.title,
                author: item.author,
                status: item.status
            });
        return requestPromise
            .then(this.onUpdateItemSuccess())
            .catch(onError);
    }

    public removeItem(id: number) {
        const requestPromise = this.configureRequest(
            `${ApiConfiguration.PRIVATE_LIST_ITEMS}/${id}`,
            'DELETE');
        return requestPromise
            .then(this.onDeleteItemSuccess(id))
            .catch(onError);
    }

    public updateListName(name: string) {
        const requestPromise = this.configureRequest(
            `${ApiConfiguration.PRIVATE_LIST}`, 'PUT', {name});
        return requestPromise
            .then(this.onListNameUpdate(name))
            .catch(onError);
    }

    private onListNameUpdate(name: string) {
        const that = this;
        return function() {
            that.dispatch(privateBookListAction.updateListName(name));
        };
    }

    private onGetListSuccess() {
        const that = this;
        return function(response: AxiosResponse) {
            const result = new RequestResult<PrivateBookListModel>(true, response.data);
            that.dispatch(privateBookListAction.setPrivateList(result.data as PrivateBookListModel));
            return result;
        };
    }

    private onAddItemSuccess() {
        const that = this;
        return function(response: AxiosResponse) {
            const result = new RequestResult<PrivateBookListItemModel>(true, response.data);
            that.dispatch(privateBookListAction.addItem(result.data as PrivateBookListItemModel));
            return result;
        };
    }

    private onUpdateItemSuccess() {
        const that = this;
        return function(response: AxiosResponse) {
            const result = new RequestResult<PrivateBookListItemModel>(true, response.data);
            that.dispatch(privateBookListAction.updateItem(result.data as PrivateBookListItemModel));
            return result;
        };
    }

    private onDeleteItemSuccess(id: number) {
        const that = this;
        return function() {
            that.dispatch(privateBookListAction.removeItem(id));
        };
    }
}