export class Chunked<T> {
    items: T[];
    chunkInfo: {
        hasNext: boolean;
        hasPrevious: boolean;
        chunk: number;
    };
}