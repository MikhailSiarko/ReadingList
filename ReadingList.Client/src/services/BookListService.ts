import { RootState } from '../store/reducers';
import { Dispatch } from 'redux';
import ApiConfiguration from '../config/ApiConfiguration';
import { loadingActions } from '../store/actions/loading';
import { AxiosResponse } from 'axios';
import { RequestResult } from '../models/Request';
import { bookListAction } from '../store/actions/bookList';
import { PrivateBookList } from '../models/BookList/Implementations/PrivateBookList';
import ApiService from './ApiService';

export class BookListService extends ApiService {
    public getPrivateList(dispatch: Dispatch<RootState>) {
        const requestPromise = this.configureRequest(ApiConfiguration.PRIVATE_LIST, 'GET');
        return requestPromise
            .then(this.onGetListSuccess(dispatch))
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

    private onError(dispatch: Dispatch<RootState>) {
        return function(error: AxiosResponse) {
            const result = error.data as RequestResult<never>;
            dispatch(loadingActions.end());
            return result;
        };
    }
}