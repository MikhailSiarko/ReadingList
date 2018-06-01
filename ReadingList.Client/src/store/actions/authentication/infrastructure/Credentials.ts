export class Credentials {
    email: string;
    password: string;
    confirmPassword: string;
    constructor(email: string, password: string, confirmPassword?: string) {
        this.email = email;
        this.password = password;
        if (confirmPassword) {
            this.confirmPassword = confirmPassword;
        }
    }
}