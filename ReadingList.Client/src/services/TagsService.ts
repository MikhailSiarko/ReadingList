// tslint:disable-next-line:quotemark
import ApiService from "./ApiService";
import ApiConfiguration from '../config/ApiConfiguration';
import { onError } from '../utils';
import { Tag } from '../models/Tag';

export class TagsService extends ApiService {
    getTags = () => {
        return this.configureRequest(ApiConfiguration.TAGS, 'GET')
            .then(this.onSuccess<Tag[]>())
            .catch(onError);
    }
}