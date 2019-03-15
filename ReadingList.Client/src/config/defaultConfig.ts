import ApiConfiguration from './apiConfig';
import { setAuthHeader } from '../utils';
import { AxiosRequestConfig } from 'axios';

export function createAxiosDefaultConfiguration(): AxiosRequestConfig {
    return {
        baseURL: ApiConfiguration.BASE_URL,
        responseType: 'json',
        headers: setAuthHeader()
    };
}