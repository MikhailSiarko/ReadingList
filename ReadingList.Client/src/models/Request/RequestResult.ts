export interface RequestResult<T> {
    isSucceed: boolean;
    errorMessage: string;
    data: T;
}