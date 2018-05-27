import axios, { AxiosPromise, AxiosResponse } from 'axios';
import { createAxiosDefaultConfiguration } from '../config/AxiosDefaultConfiguration';
import { failed } from '../utils';

abstract class ApiService {
    protected configureRequest<T>(url: string, method: string, data?: T): AxiosPromise<any> {
        const axiosInstance = axios.create(createAxiosDefaultConfiguration());
        return axiosInstance.request({
            url: url,
            method: method,
            data: data
        }).then((response: AxiosResponse) => response)
            .catch(failed);
    }
}

export default ApiService;