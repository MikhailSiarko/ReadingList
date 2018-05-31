import axios, { AxiosPromise, AxiosResponse } from 'axios';
import { createAxiosDefaultConfiguration } from '../config/AxiosDefaultConfiguration';
import { failed } from '../utils';
import { Dispatch } from 'react-redux';
import { RootState } from '../store/reducers';
import { loadingActions } from '../store/actions/loading';

abstract class ApiService {
    protected dispatch: Dispatch<RootState>;
    constructor(dispatch: Dispatch<RootState>) {
        this.dispatch = dispatch;
    }
    protected configureRequest<T>(url: string, method: string, data?: T): AxiosPromise<any> {
        const axiosInstance = axios.create(createAxiosDefaultConfiguration());
        axiosInstance.interceptors.request.use(config => {
            this.dispatch(loadingActions.start());
            return config;
        });
        axiosInstance.interceptors.response.use(response => {
            this.dispatch(loadingActions.end());
            return response;
        });
        return axiosInstance.request({
            url: url,
            method: method,
            data: data
        }).then((response: AxiosResponse) => response)
            .catch(failed);
    }
}

export default ApiService;