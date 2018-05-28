import { RootState } from '../store/reducers';
import { Dispatch } from 'redux';
import ApiConfiguration from '../config/ApiConfiguration';
import { loadingActions } from '../store/actions/loading';
import { AxiosResponse } from 'axios';
import { RequestResult } from '../models/Request';
import { bookListAction } from '../store/actions/bookList';
import { PrivateBookList } from '../models/BookList/Implementations/PrivateBookList';
import ApiService from './ApiService';
import { PrivateBookListItem } from '../models/BookList/Implementations/PrivateBookListItem';

export class PrivateBookListService extends ApiService {
    public getList(dispatch: Dispatch<RootState>) {
        const requestPromise = this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'GET');
        return requestPromise
            .then(this.onGetListSuccess(dispatch))
            .catch(this.onError(dispatch));
    }

    public addItem(dispatch: Dispatch<RootState>, item: PrivateBookListItem) {
        const requestPromise = this.configureRequest(`${ApiConfiguration.PRIVATE_LIST_ITEMS}`,
            'POST', {title: item.title, author: item.author});
        return requestPromise
            .then(this.onAddItemSuccess(dispatch))
            .catch(this.onError(dispatch));
    }

    public updateItem(dispatch: Dispatch<RootState>, item: PrivateBookListItem) {
        const requestPromise = this.configureRequest(
            `${ApiConfiguration.PRIVATE_LIST_ITEMS}/${item.id}`,
            'PUT', {
                title: item.title,
                author: item.author,
                status: item.status
            });
        return requestPromise
            .then(this.onUpdateItemSuccess(dispatch))
            .catch(this.onError(dispatch));
    }

    public removeItem(dispatch: Dispatch<RootState>, id: number) {
        const requestPromise = this.configureRequest(
            `${ApiConfiguration.PRIVATE_LIST_ITEMS}/${id}`,
            'DELETE');
        return requestPromise
            .then(this.onDeleteItemSuccess(dispatch, id))
            .catch(this.onError(dispatch));
    }

    private onGetListSuccess(dispatch: Dispatch<RootState>) {
        return function(response: AxiosResponse) {
            const result = response.data as RequestResult<PrivateBookList>;
            dispatch(bookListAction.setPrivateList(result.data as PrivateBookList));
            dispatch(loadingActions.end());
            return result;
        };
    }

    private onAddItemSuccess(dispatch: Dispatch<RootState>) {
        return function(response: AxiosResponse) {
            const result = response.data as RequestResult<PrivateBookListItem>;
            dispatch(bookListAction.addItem(result.data as PrivateBookListItem));
            dispatch(loadingActions.end());
            return result;
        };
    }

    private onUpdateItemSuccess(dispatch: Dispatch<RootState>) {
        return function(response: AxiosResponse) {
            const result = response.data as RequestResult<PrivateBookListItem>;
            dispatch(bookListAction.updateItem(result.data as PrivateBookListItem));
            dispatch(loadingActions.end());
            return result;
        };
    }

    private onDeleteItemSuccess(dispatch: Dispatch<RootState>, id: number) {
        return function() {
            dispatch(bookListAction.removeItem(id));
            dispatch(loadingActions.end());
        };
    }

    private onError(dispatch: Dispatch<RootState>) {
        return function(error: AxiosResponse) {
            const result = error.data as RequestResult<never>;
            dispatch(loadingActions.end());
            return result;
        };
    }
}