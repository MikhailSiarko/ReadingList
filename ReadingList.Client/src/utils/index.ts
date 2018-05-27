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