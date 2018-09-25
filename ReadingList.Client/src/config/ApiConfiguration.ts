export class ApiConfiguration {
    public static BASE_URL = 'http://localhost:50582/api';
    public static LOGIN = '/account/login';
    public static REGISTER = '/account/register';
    public static LIST = '/list';
    public static PRIVATE_LIST = `${ApiConfiguration.LIST}/private`;
    public static PRIVATE_LIST_ITEMS = `${ApiConfiguration.LIST}/private/items`;
    public static SHARED_LISTS = `${ApiConfiguration.LIST}/shared`;
    public static BOOK_STATUSES = '/bookstatuses';

    public static getPrivateListItemUrl(itemId: number) {
        return `${ApiConfiguration.PRIVATE_LIST}/items/${itemId}`;
    }

    public static getSharedListUrl(listId: number) {
        return `${ApiConfiguration.LIST}/shared/${listId}`;
    }

    public static getSharedListItemUrl(listId: number, itemId: number) {
        return `${ApiConfiguration.LIST}/shared/${listId}/items/${itemId}`;
    }
}

export default ApiConfiguration;