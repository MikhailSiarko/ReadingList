import axios, { AxiosResponse } from 'axios';
import { createAxiosDefaultConfiguration } from '../config/AxiosDefaultConfiguration';
import { RequestResult } from '../models';

abstract class ApiService {
    protected onSuccess<TOut>() {
        return (response: AxiosResponse) => new RequestResult<TOut>(true, response.data);
    }

    protected configureRequest<TData>(url: string, method: string, data?: TData) {
        const axiosInstance = axios.create(createAxiosDefaultConfiguration());

        axiosInstance.interceptors.request.use(config => config, error => {
            let result = new RequestResult<never>(false, undefined,
                error.response ? error.response.data.errorMessage : error.message);
            return Promise.reject(result);
        });

        axiosInstance.interceptors.response.use(response => response, error => {
            let result = new RequestResult<never>(
                false,
                undefined,
                error.response
                    ? error.response.data.errorMessage
                    : error.message,
                error.response ? error.response.status : undefined
            );

            return Promise.reject(result);
        });

        return axiosInstance.request({
            url: url,
            method: method,
            data: data
        });
    }
}

export default ApiService;