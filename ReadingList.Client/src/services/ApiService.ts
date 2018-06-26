import axios from 'axios';
import { createAxiosDefaultConfiguration } from '../config/AxiosDefaultConfiguration';
import { Dispatch } from 'react-redux';
import { RootState } from '../store/reducers';
import { loadingActions } from '../store/actions/loading';
import { RequestResult } from '../models';

abstract class ApiService {
    protected dispatch: Dispatch<RootState>;
    protected constructor(dispatch: Dispatch<RootState>) {
        this.dispatch = dispatch;
    }
    protected configureRequest<T>(url: string, method: string, data?: T) {
        const axiosInstance = axios.create(createAxiosDefaultConfiguration());
        axiosInstance.interceptors.request.use(config => {
            this.dispatch(loadingActions.start());
            return config;
        }, error => {
            this.dispatch(loadingActions.end());
            let result = new RequestResult<never>(false, undefined,
                error.response ? error.response.data.errorMessage : error.message);
            return Promise.reject(result);
        });
        axiosInstance.interceptors.response.use(response => {
            this.dispatch(loadingActions.end());
            return response;
        }, error => {
            this.dispatch(loadingActions.end());
            if(error.response && error.response.status === 401) {
                error.response.data = new RequestResult<never>(false, undefined, 'You are not authenticate');
            }
            let result = new RequestResult<never>(false, undefined,
                error.response ? error.response.data.errorMessage : error.message);
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