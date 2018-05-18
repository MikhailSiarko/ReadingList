import { UserModel } from '../../../../models';

export class AuthenticationData {
    token: string;
    user: UserModel;
    constructor(token: string, user: UserModel) {
        this.token = token;
        this.user = user;
    }
}