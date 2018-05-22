export class RequestInfo {
    method: string;
    url: string;
    data: any;
    constructor(method?: string, url?: string, data?: any) {
        if(method) {
            this.method = method;
        }
        if(url) {
            this.url = url;
        }
        if(data) {
            this.data = data;
        }
    }
}