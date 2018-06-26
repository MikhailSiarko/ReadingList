export class RequestResult<T> {
    isSucceed: boolean;
    errorMessage?: string;
    data?: T;
    constructor(isSucceed: boolean, data?: T, errorMessage?: string) {
        this.isSucceed = isSucceed;
        this.errorMessage = errorMessage;
        this.data = data;
    }
}