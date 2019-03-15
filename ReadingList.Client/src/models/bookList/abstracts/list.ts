import { ListItem } from './listItem';

export class List<T extends ListItem> {
    id: number;
    name: string;
    type: number;
    isInEditMode: boolean = false;
    items: T[];

    static removeItem<TItem extends ListItem>(list: List<TItem>, itemId: number) {
        const itemIndex = list.items.findIndex(List.itemPredicate(itemId));
        list.items.splice(itemIndex, 1);
    }

    static updateItem<TItem extends ListItem>(list: List<TItem>, item: TItem) {
        const index = list.items.findIndex(List.itemPredicate(item.id));
        list.items[index] = item;
    }

    static add<TItem extends ListItem>(list: List<TItem>, item: TItem) {
        list.items.push(item);
    }

    static switchMode<TItem extends ListItem>(list: List<TItem>) {
        list.isInEditMode = !list.isInEditMode;
    }

    private static itemPredicate<TItem extends ListItem>(itemId: number) {
        return (item: TItem) => item.id === itemId;
    }
}