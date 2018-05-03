import ApiConfiguration from './ApiConfiguration';
import { setAuthHeader } from '../utils';

const axiosDefaultConfiguration = {
    baseURL: ApiConfiguration.baseURL,
    responseType: 'json',
    headers: setAuthHeader()
};

export default axiosDefaultConfiguration;