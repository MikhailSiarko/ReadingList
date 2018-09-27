import ApiService from './ApiService';
import { RootState } from '../store/reducers';
import { Dispatch } from 'redux';
import { ApiConfiguration } from '../config/ApiConfiguration';
import { onError } from '../utils';
import { AxiosResponse } from 'axios';
import { SharedBookList } from '../models/BookList/Implementations/SharedBookList';
import { RequestResult } from '../models';

export class SharedBookListService extends ApiService {
    constructor(dispatch: Dispatch<RootState>) {
        super(dispatch);
    }

    public getOwnLists() {
        const requestPromise = this.configureRequest(ApiConfiguration.SHARED_LISTS_OWN, 'GET');
        return requestPromise
            .then(this.onGetOwnListsSuccess)
            .catch(onError);
    }

    public getLists(query: string) {
        const requestPromise = this.configureRequest(ApiConfiguration.getFindSharedListsUrl(query), 'GET');
        return requestPromise
            .then(this.onGetOwnListsSuccess)
            .catch(onError);
    }

    private onGetOwnListsSuccess(response: AxiosResponse) {
        const result = new RequestResult<SharedBookList[]>(true, response.data);
        return result;
    }
}