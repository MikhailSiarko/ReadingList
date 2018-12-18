// tslint:disable-next-line:quotemark
import ApiService from "./ApiService";
import ApiConfiguration from '../config/ApiConfiguration';
import { SelectListItem } from '../models';
import { onError } from '../utils';

export class TagsService extends ApiService {
    getTags = () => {
        return this.configureRequest(ApiConfiguration.TAGS, 'GET')
            .then(this.onSuccess<SelectListItem[]>())
            .catch(onError);
    }
}