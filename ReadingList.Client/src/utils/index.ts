export function setAuthHeader() {
    let token = sessionStorage.getItem('reading_list');
    if (token) {
        return {'Authorization': 'Bearer ' + token};
    } else {
        return {};
    }
}

export function isNullOrEmpty(str: string | null | undefined) {
    return str === null || str === undefined || str.length === 0;
}

export function reduceTags(tags: string[]) {
    return tags.reduce((acc, tag) => acc + ' #' + tag, '').substring(1);
}