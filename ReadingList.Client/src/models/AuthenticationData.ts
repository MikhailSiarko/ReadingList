import { UserModel } from './';

export interface AuthenticationData {
    token: string;
    user: UserModel;
}