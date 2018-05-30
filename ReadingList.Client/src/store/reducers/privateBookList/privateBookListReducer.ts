import { RootState } from '../index';
import initialState from '../initialState';
import { getType } from 'typesafe-actions';
import { PrivateBookListAction, privateBookListAction } from '../../actions/privateBookList';
import { PrivateBookListItemModel, PrivateBookListModel } from '../../../models';
import { cloneDeep } from 'lodash';

export function privateBookListReducer(state: RootState.PrivateList = initialState.privateList,
                                      action: PrivateBookListAction) {
    switch (action.type) {
        case getType(privateBookListAction.setPrivateList):
            return action.list;
        case getType(privateBookListAction.unsetPrivateList):
            return initialState.privateList;
        case getType(privateBookListAction.switchEditModeForList):
            const dc = cloneDeep(state as PrivateBookListModel);
            dc.isInEditMode = !dc.isInEditMode;
            return dc;
        case getType(privateBookListAction.updateListName):
            const cd = cloneDeep(state as PrivateBookListModel);
            cd.name = action.newName;
            cd.isInEditMode = false;
            return cd;
        case getType(privateBookListAction.addItem):
            const copy = cloneDeep(state as PrivateBookListModel);
            copy.items.push(action.listItem);
            return copy;
        case getType(privateBookListAction.removeItem):
            let deepCopy = cloneDeep(state as PrivateBookListModel);
            const itemIndex = deepCopy.items
                .findIndex((listItem: PrivateBookListItemModel) => listItem.id === action.itemId);
            deepCopy.items.splice(itemIndex, 1);
            return deepCopy;
        case getType(privateBookListAction.updateItem):
            const clone = cloneDeep(state as PrivateBookListModel);
            const index = clone.items.findIndex(
                ((listItem: PrivateBookListItemModel) => listItem.id === action.item.id));
            clone.items[index] = action.item;
            return clone;
        case getType(privateBookListAction.switchEditModeForItem):
            const deepClone = cloneDeep(state as PrivateBookListModel);
            deepClone.items.forEach((listItem: PrivateBookListItemModel) => {
                if(listItem.id === action.itemId) {
                    listItem.isOnEditMode = !listItem.isOnEditMode;
                }
            });
            return deepClone;
        default:
            return state;
    }
}