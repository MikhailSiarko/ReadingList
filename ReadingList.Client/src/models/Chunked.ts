export class Chunked<T> {
    items: T[];
    hasNext: boolean;
    hasPrevious: boolean;
    chunk: number;
}