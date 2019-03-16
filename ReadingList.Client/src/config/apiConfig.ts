export class ApiConfiguration {
    public static BASE_URL = 'http://localhost:50582/api';
    public static LOGIN = '/account/login';
    public static REGISTER = '/account/register';
    public static LISTS = '/lists';
    public static MODERATED_LISTS = `${ApiConfiguration.LISTS}/moderated`;
    public static PRIVATE_LIST = `${ApiConfiguration.LISTS}/private`;
    public static PRIVATE_LIST_ITEMS = `${ApiConfiguration.LISTS}/private/items`;
    public static SHARED_LISTS = `${ApiConfiguration.LISTS}/shared`;
    public static SHARED_LISTS_MY = `${ApiConfiguration.SHARED_LISTS}/my`;
    public static BOOK_STATUSES = '/bookstatuses';
    public static BOOKS = '/books';
    public static TAGS = '/tags';
    public static USERS = '/users';

    public static getPrivateListItemUrl(itemId: number) {
        return `${ApiConfiguration.PRIVATE_LIST}/items/${itemId}`;
    }

    public static getSharedListUrl(listId: number) {
        return `${ApiConfiguration.SHARED_LISTS}/${listId}`;
    }

    public static getSharedListItemUrl(listId: number, itemId: number) {
        return `${ApiConfiguration.SHARED_LISTS}/${listId}/items/${itemId}`;
    }

    public static getFindSharedListsUrl(query: string, chunk: number | null, count: number | null) {
        return `${ApiConfiguration.SHARED_LISTS}?query=${encodeURIComponent(query)}&chunk=${chunk}&count=${count}`;
    }

    public static getMySharedListsUrl(chunk: number | null, count: number | null) {
        return `${ApiConfiguration.SHARED_LISTS_MY}?chunk=${chunk}&count=${count}`;
    }

    public static getAddItemToSharedListUrl(listId: number) {
        return `${ApiConfiguration.SHARED_LISTS}/${listId}/items`;
    }

    public static getBooksSearchUrl(query: string, chunk: number | null) {
        return `${ApiConfiguration.BOOKS}?query=${encodeURIComponent(query)}&chunk=${chunk}`;
    }

    public static getSharePrivateListUrl(name: string) {
        return `${ApiConfiguration.PRIVATE_LIST}/share/${name}`;
    }

    public static getShareBookUrl(bookId: number) {
        return `${ApiConfiguration.BOOKS}/` + bookId;
    }
}

export default ApiConfiguration;