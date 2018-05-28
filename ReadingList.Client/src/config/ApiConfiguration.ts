export class ApiConfiguration {
    public static BASE_URL = 'http://localhost:5000/api';
    public static LOGIN = '/account/login';
    public static REGISTER = '/account/register';
    public static PRIVATE_LIST = '/privatelist';
    public static PRIVATE_LIST_ITEMS = `${ApiConfiguration.PRIVATE_LIST}/items`;
}

export default ApiConfiguration;