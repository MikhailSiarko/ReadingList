import { User } from '.';

export interface AuthenticationData {
    token: string;
    user: User;
}