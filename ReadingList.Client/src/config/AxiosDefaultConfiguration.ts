import ApiConfiguration from './ApiConfiguration';
import { setAuthHeader } from '../utils';

export function createAxiosDefaultConfiguration() {
    return {
        baseURL: ApiConfiguration.BASE_URL,
        responseType: 'json',
        headers: setAuthHeader()
    };
}