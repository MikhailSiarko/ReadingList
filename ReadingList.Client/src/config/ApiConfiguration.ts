export class ApiConfiguration {
    public static BASE_URL = 'http://localhost:50582/api';
    public static LOGIN = '/account/login';
    public static REGISTER = '/account/register';
    public static LISTS = '/lists';
    public static PRIVATE_LIST = `${ApiConfiguration.LISTS}/private`;
    public static PRIVATE_LIST_ITEMS = `${ApiConfiguration.LISTS}/private/items`;
    public static SHARED_LISTS = `${ApiConfiguration.LISTS}/shared`;
    public static SHARED_LISTS_OWN = `${ApiConfiguration.SHARED_LISTS}/own`;
    public static BOOK_STATUSES = '/bookstatuses';
    public static BOOKS = '/books';
    public static TAGS = '/tags';
    public static USERS = '/users';

    public static getPrivateListItemUrl(itemId: number) {
        return `${ApiConfiguration.PRIVATE_LIST}/items/${itemId}`;
    }

    public static getSharedListUrl(listId: number) {
        return `${ApiConfiguration.LISTS}/shared/${listId}`;
    }

    public static getSharedListItemUrl(listId: number, itemId: number) {
        return `${ApiConfiguration.LISTS}/shared/${listId}/items/${itemId}`;
    }

    public static getFindSharedListsUrl(query: string) {
        return `${ApiConfiguration.SHARED_LISTS}?query=${encodeURIComponent(query)}`;
    }

    public static getAddItemToSharedListUrl(listId: number) {
        return `${ApiConfiguration.LISTS}/shared/${listId}/items`;
    }

    public static getBooksSearchUrl(query: string) {
        return `${ApiConfiguration.BOOKS}?query=${encodeURIComponent(query)}`;
    }

    public static getSharePrivateListUrl(name: string) {
        return `${ApiConfiguration.PRIVATE_LIST}/share?name=${name}`;
    }
}

export default ApiConfiguration;