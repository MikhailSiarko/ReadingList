import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { PrivateListAction, privateListActions } from './actions';
import { PrivateBookListItem } from 'src/models';
import { cloneDeep } from 'lodash';

export function privateBookListReducer(state: RootState.Private = initialState.private, action: PrivateListAction) {
    const copy = cloneDeep(state);
    switch (action.type) {
        case getType(privateListActions.unsetPrivate):
            return initialState.private;
        case getType(privateListActions.switchListEditMode):
            if (copy.list) {
                copy.list.isInEditMode = !copy.list.isInEditMode;
            }
            return copy;
        case getType(privateListActions.updateListSuccess):
            copy.list = action.payload;
            return copy;
        case getType(privateListActions.addItemSuccess):
            if (copy.list) {
                copy.list.items.push(action.payload);
            }
            return copy;
        case getType(privateListActions.removeItemSuccess):
            if (copy.list) {
                const itemIndex = copy.list.items
                    .findIndex((listItem: PrivateBookListItem) => listItem.id === action.payload);
                copy.list.items.splice(itemIndex, 1);
            }
            return copy;
        case getType(privateListActions.updateItemSuccess):
            if (copy.list) {
                const index = copy.list.items.findIndex(
                    ((listItem: PrivateBookListItem) => listItem.id === action.payload.id));
                copy.list.items[index] = action.payload;
            }
            return copy;
        case getType(privateListActions.switchItemEditMode):
            if (copy.list) {
                copy.list.items.forEach((listItem: PrivateBookListItem) => {
                    if (listItem.id === action.payload) {
                        listItem.isInEditMode = !listItem.isInEditMode;
                    }
                });
            }
            return copy;
        case getType(privateListActions.fetchItemStatusesSuccess):
            copy.bookStatuses = action.payload;
            return copy;
        default:
            return state;
    }
}