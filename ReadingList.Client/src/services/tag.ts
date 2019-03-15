import ApiService from './api';
import ApiConfiguration from '../config/apiConfig';
import { onError } from '../utils';
import { Tag } from '../models';

export class TagsService extends ApiService {
    getTags = () => {
        return this.configureRequest(ApiConfiguration.TAGS, 'GET')
            .then(this.onSuccess<Tag[]>())
            .catch(onError);
    }
}