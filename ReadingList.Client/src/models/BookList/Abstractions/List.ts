import { ListType } from './ListType';
import { ListItem } from './ListItem';

export interface List<T extends ListItem<any>> {
    id: string;
    items: T[];
    description: string;
    tags: string[];
    type: ListType;
}