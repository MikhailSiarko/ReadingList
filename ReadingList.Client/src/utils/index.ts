import { RequestResult } from '../models';
import { cloneDeep } from 'lodash';

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
  var copy = cloneDeep(props) as any;
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