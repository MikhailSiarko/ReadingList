import { UserModel } from '../../../../models';

export interface AuthenticationData {
    token: string;
    user: UserModel;
}