import ApiService from './ApiService';
import ApiConfiguration from '../config/ApiConfiguration';
import { onError } from '../utils';
import { Book } from '../models';

export class BookService extends ApiService {
    findBooks = (query: string) => {
        return this.configureRequest(ApiConfiguration.getBooksSearchUrl(query), 'GET')
            .then(this.onSuccess<Book[]>())
            .catch(onError);
    }

    shareBook = (bookId: number, listsIds: number[]) => {
        return this.configureRequest(ApiConfiguration.getShareBookUrl(bookId), 'POST', listsIds)
            .then(this.onSuccess<void>())
            .catch(onError);
    }
}