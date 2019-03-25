import ApiService from './api';
import ApiConfiguration from '../config/apiConfig';
import { onError } from '../utils';
import { Book, Chunked } from '../models';

export class BookService extends ApiService {
    findBooks = (query: string, chunk: number | null) => {
        return this.configureRequest(ApiConfiguration.BOOKS, 'GET', undefined, { query, chunk })
            .then(this.onSuccess<Chunked<Book>>())
            .catch(onError);
    }

    shareBook = (bookId: number, listsIds: number[]) => {
        return this.configureRequest(ApiConfiguration.BOOKS, 'POST', listsIds, { bookId })
            .then(this.onSuccess<void>())
            .catch(onError);
    }
}