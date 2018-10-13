export class RequestResult<T> {
    isSucceed: boolean;
    errorMessage?: string;
    status?: number;
    data?: T;
    constructor(isSucceed: boolean, data?: T, errorMessage?: string, status?: number) {
        this.isSucceed = isSucceed;
        this.errorMessage = errorMessage;
        this.data = data;
        this.status = status;
    }
}