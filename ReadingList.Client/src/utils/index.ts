import { RequestResult } from '../models';
import { cloneDeep } from 'lodash';
import { Dispatch } from 'react-redux';
import { RootState } from '../store/reducers';
import { authenticationActions } from '../store/actions/authentication';

export function onError(error: RequestResult<never>) {
  return error;
}

export function setAuthHeader() {
  let token = sessionStorage.getItem('reading_list');
  if (token) {
    return { 'Authorization': 'Bearer ' + token };
  } else {
    return {};
  }
}

export function deleteProperties(object: Object, properties: string[]) {
  for (const property of properties) {
    delete object[property];
  }
}

export function createDOMAttributeProps(props: Object, ...propertiesToDelete: string[]) {
  let copy = cloneDeep(props) as any;
  deleteProperties(copy, propertiesToDelete);
  return copy;
}

export function isNullOrEmpty(str: string | null | undefined) {
  return str === null || str === undefined || str.length === 0;
}

export function convertSecondsToReadingTime(seconds: number): string {
  let days: any = Math.floor(seconds / 86400);
  let hours: any = Math.floor((seconds - (days * 86400)) / 3600);
  let minutes: any = Math.floor((seconds - (hours * 3600) - (days * 86400)) / 60);

  if (hours < 10) {
      hours = '0' + hours;
  }
  if (minutes < 10) {
      minutes = '0' + minutes;
  }
  return `days: ${days} | hours: ${hours} | minutes: ${minutes}`;
}

export function postRequestProcess(result: RequestResult<any>, dispatch: Dispatch<RootState>) {
  if(!result.isSucceed && result.status && result.status === 401) {
    dispatch(authenticationActions.signOut());
  } else if(!result.isSucceed) {
      alert(result.errorMessage);
  }
}

export function applyClasses(...classes: string[]) {
  return classes.join(' ');
}

export function createPropAction<TIn, TOut>(func: (data: TIn) => Promise<RequestResult<TOut>>,
                dispatch: Dispatch<RootState>, action?: (out: TOut) => any) {
    return async function(inner: TIn) {
        const result = await func(inner);
        if(result.isSucceed && result.data && action) {
            dispatch(action(result.data));
        } else {
            postRequestProcess(result, dispatch);
        }
    };
}

export function createPropActionWithResult<TIn, TOut>(func: (data: TIn) => Promise<RequestResult<TOut>>,
                dispatch: Dispatch<RootState>, action?: (out: TOut) => any) {
    return async function(inner: TIn) {
        const result = await func(inner);
        if(result.isSucceed && result.data && action) {
            dispatch(action(result.data));
        } else {
            postRequestProcess(result, dispatch);
        }
        return result.data as TOut;
    };
}

export function reduceTags(tags: string[]) {
  return tags.reduce((acc, tag) => acc + ' #' + tag, '').substring(1);
}