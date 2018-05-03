import { AxiosResponse } from 'axios';
import { RequestError, RequestInfo } from './infrastructure';
import { RequestActionType } from './RequestActionType';
import { createAction, getReturnOfExpression } from 'typesafe-actions';

export const requestActions = {
    begin: createAction(RequestActionType.REQUEST_BEGIN, (info: RequestInfo) => {
        return {
            type: RequestActionType.REQUEST_BEGIN,
            info
        };
    }),
    success: createAction(RequestActionType.REQUEST_SUCCESS, (response: AxiosResponse) => {
        return {
            type: RequestActionType.REQUEST_SUCCESS,
            response
        };
    }),
    failed: createAction(RequestActionType.REQUEST_FAILED, (requestError: RequestError) => {
        return {
            type: RequestActionType.REQUEST_FAILED,
            requestError
        };
    })
};

const returnOfActions = Object.values(requestActions).map(getReturnOfExpression);
export type RequestAction = typeof returnOfActions[number];