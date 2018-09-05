export class ApiConfiguration {
    public static BASE_URL = 'http://localhost:50582/api';
    public static LOGIN = '/account/login';
    public static REGISTER = '/account/register';
    public static LIST = '/list';
    public static PRIVATE_LIST = `${ApiConfiguration.LIST}/private`;
    public static PRIVATE_LIST_ITEMS = `${ApiConfiguration.PRIVATE_LIST}/items`;
}

export default ApiConfiguration;