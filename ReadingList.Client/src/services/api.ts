import axios, { AxiosResponse } from 'axios';
import { createAxiosDefaultConfiguration } from '../config/defaultConfig';
import { RequestResult } from '../models';

abstract class ApiService {
    protected onSuccess<TOut>() {
        return (response: AxiosResponse) => new RequestResult<TOut>(true, response.data);
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
        let result = new RequestResult<never>(
            false,
            undefined,
            error.response
                ? error.response.data ? error.response.data.errorMessage : error.message
                : error.message,
            error.response ? error.response.status : undefined
        );
        return Promise.reject(result);
    }
}

export default ApiService;