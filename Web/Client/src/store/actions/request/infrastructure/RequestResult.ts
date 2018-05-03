export class RequestResult<T> {
    message: string;
    status: number;
    data: T;
    constructor(message: string, status: number, data?: T) {
        this.message = message;
        this.status = status;
        if(data) {
            this.data = data;
        }
    }

    public isSuccess(): boolean {
        if(this.status >= 100 && this.status < 400) {
            return true;
        } else {
            return false;
        }
    }
}