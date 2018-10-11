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

    getOwnLists() {
        return this.configureRequest(ApiConfiguration.SHARED_LISTS_OWN, 'GET')
            .then(this.onGetOwnListsSuccess)
            .catch(onError);
    }

    getLists(query: string) {
        return this.configureRequest(ApiConfiguration.getFindSharedListsUrl(query), 'GET')
            .then(this.onGetOwnListsSuccess)
            .catch(onError);
    }

    private onGetOwnListsSuccess(response: AxiosResponse) {
        const result = new RequestResult<SharedBookList[]>(true, response.data);
        return result;
    }
}