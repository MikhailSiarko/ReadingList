import { Component } from 'react-redux';

export function failed(error: any) {
  return Promise.reject(error && error.response);
}

export function setAuthHeader() {
  let token = sessionStorage.getItem('token');
  if (token) {
    return { 'Authorization': 'Bearer ' + token };
  } else {
    return {};
  }
}

export function isNullOrEmpty(str: string | null | undefined) {
  return str === null || str === undefined || str.length === 0;
}

export type AppElement = string | Component<any>;

export function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
          .toString(16)
          .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}