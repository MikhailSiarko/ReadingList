import ApiConfiguration from '../config/ApiConfiguration';
import axiosDefaultConfig from '../config/AxiosDefaultConfiguration';
import axios, { AxiosPromise, AxiosResponse } from 'axios';
import { failed } from '../utils';
import { Credentials } from '../store/actions/authentication/infrastructure';
import { Dispatch } from 'redux';
import { RootState } from '../store/reducers';
import { requestActions, RequestInfo } from '../store/actions/request';
import { authenticationActions, AuthenticationData } from '../store/actions/authentication';
import { UserModel } from '../models';
import { RequestResult } from '../store/actions/request/infrastructure';

export class AuthenticationService {
    static login(dispatch: Dispatch<RootState>, credentials: Credentials) {
        const requestPromise = AuthenticationService.configureRequest(dispatch, ApiConfiguration.login, credentials);
        return AuthenticationService.configurePromise(requestPromise, dispatch);
    }

    static register(dispatch: Dispatch<RootState>, credentials: Credentials) {
        const requestPromise = AuthenticationService.configureRequest(dispatch, ApiConfiguration.register, credentials);
        return AuthenticationService.configurePromise(requestPromise, dispatch);
    }

    private static configureRequest(dispatch: Dispatch<RootState>, url: string, credentials: Credentials) {
        const axiosDefault = axios.create(axiosDefaultConfig);
        axiosDefault.interceptors.request.use((config) => {
            dispatch(requestActions.begin(new RequestInfo(config.method, ApiConfiguration.register, credentials)));
            return config;
        });
        return axiosDefault.post(url, credentials)
            .then((response: AxiosResponse) => response)
            .catch(failed);
    }

    private static onSuccess(dispatch: Dispatch<RootState>) {
        return function(response: AxiosResponse) {
            const result = new RequestResult<never>(response.statusText, response.status);
            dispatch(requestActions.success(result));
            dispatch(authenticationActions.signIn(new AuthenticationData(response.data.token,
                response.data.user as UserModel)));
            return result;
        };
    }

    private static onError(dispatch: Dispatch<RootState>) {
        return function(error: AxiosResponse) {
            const result = new RequestResult<never>(error.data.errorMessage, error.status);
            dispatch(requestActions.failed(result));
            return result;
        };
    }

    private static configurePromise(requestPromise: AxiosPromise, dispatch: Dispatch<RootState>) {
        return requestPromise
            .then(AuthenticationService.onSuccess(dispatch))
            .catch(AuthenticationService.onError(dispatch));
    }
}
