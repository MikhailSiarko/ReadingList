export class RequestError {
    message: string;
    status: number;
    constructor(message: string, status?: number) {
        this.message = message;
        if(status) {
            this.status = status;
        }
    }
}