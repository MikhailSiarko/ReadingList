import { RootState } from '../index';
import initialState from '../initialState';
import { getType } from 'typesafe-actions';
import { PrivateBookListAction, privateBookListAction } from '../../actions/privateBookList';
import { PrivateBookListItem } from '../../../models';
import { cloneDeep } from 'lodash';

export function privateBookListReducer(state: RootState.Private = initialState.private,
                                      action: PrivateBookListAction) {
    const copy = cloneDeep(state);
    switch (action.type) {
        case getType(privateBookListAction.setPrivate):
            copy.list = action.list;
            return copy;
        case getType(privateBookListAction.unsetPrivate):
            return initialState.private;
        case getType(privateBookListAction.switchEditModeForList):
            if(copy.list) {
                copy.list.isInEditMode = !copy.list.isInEditMode;
            }
            return copy;
        case getType(privateBookListAction.updateListName):
            if(copy.list) {
                copy.list.name = action.newName;
                copy.list.isInEditMode = false;
            }
            return copy;
        case getType(privateBookListAction.addItem):
            if(copy.list) {
                copy.list.items.push(action.listItem);
            }
            return copy;
        case getType(privateBookListAction.removeItem):
            if(copy.list) {
                const itemIndex = copy.list.items
                .findIndex((listItem: PrivateBookListItem) => listItem.id === action.itemId);
                copy.list.items.splice(itemIndex, 1);
            }
            return copy;
        case getType(privateBookListAction.updateItem):
            if(copy.list) {
                const index = copy.list.items.findIndex(
                    ((listItem: PrivateBookListItem) => listItem.id === action.item.id));
                    copy.list.items[index] = action.item;
            }
            return copy;
        case getType(privateBookListAction.switchEditModeForItem):
            if(copy.list) {
                copy.list.items.forEach((listItem: PrivateBookListItem) => {
                    if(listItem.id === action.itemId) {
                        listItem.isOnEditMode = !listItem.isOnEditMode;
                    }
                });
            }
            return copy;
        case getType(privateBookListAction.setBookStatuses):
            copy.bookStatuses = action.statuses;
            return copy;
        default:
            return state;
    }
}