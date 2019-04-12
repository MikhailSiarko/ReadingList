import axios, { AxiosResponse } from 'axios';
import { createAxiosDefaultConfiguration } from '../config/defaultConfig';
import { RequestResult } from '../models';
import { get } from 'lodash';

abstract class ApiService {
    protected onSuccess<TOut>() {
        return (response: AxiosResponse) => new RequestResult<TOut>(true, response.data);
    }

    protected onError(error: RequestResult<never>) {
        return error;
    }

    protected configureRequest<TData, TParams>(url: string, method: string, data?: TData, params?: TParams) {
        const axiosInstance = axios.create(createAxiosDefaultConfiguration());

        axiosInstance.interceptors.request.use(
            config => config,
            this.handleError
        );

        axiosInstance.interceptors.response.use(
            response => response,
            this.handleError
        );

        return axiosInstance.request({
            url,
            method,
            data,
            params
        });
    }

    private handleError = (error: any) => {
        const result = new RequestResult<never>(
            false,
            undefined,
            get(error, 'response.data.errorMessage', error.message),
            get(error, 'response.status')
        );
        return Promise.reject(result);
    }
}

export default ApiService;