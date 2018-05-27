import { ListItem } from './ListItem';

export interface List<T extends ListItem> {
    id: number;
    name: string;
    items: T[];
    type: number;
}