import { createAction, getReturnOfExpression } from 'typesafe-actions';
import { PrivateBookListActionType } from './PrivateBookListActionType';
import { PrivateBookListItemModel, PrivateBookListModel, SelectListItem } from '../../../models';

export const privateBookListAction = {
    setPrivate: createAction(PrivateBookListActionType.SET_PRIVATE_LIST, (list: PrivateBookListModel) => {
        return {
            type: PrivateBookListActionType.SET_PRIVATE_LIST,
            list
        };
    }),
    unsetPrivate: createAction(PrivateBookListActionType.UNSET_PRIVATE_LIST, () => {
        return {
            type: PrivateBookListActionType.UNSET_PRIVATE_LIST,
        };
    }),
    switchEditModeForList: createAction(PrivateBookListActionType.SWITCH_LIST_EDIT_MODE, () => {
        return {
            type: PrivateBookListActionType.SWITCH_LIST_EDIT_MODE
        };
    }),
    updateListName: createAction(PrivateBookListActionType.UPDATE_LIST_NAME, (newName: string) => {
        return {
            type: PrivateBookListActionType.UPDATE_LIST_NAME,
            newName
        };
    }),
    addItem: createAction(PrivateBookListActionType.ADD_ITEM, (listItem: PrivateBookListItemModel) => {
        return {
            type: PrivateBookListActionType.ADD_ITEM,
            listItem
        };
    }),
    removeItem: createAction(PrivateBookListActionType.REMOVE_ITEM, (itemId: number) => {
        return {
            type: PrivateBookListActionType.REMOVE_ITEM,
            itemId
        };
    }),
    updateItem: createAction(PrivateBookListActionType.UPDATE_ITEM, (item: PrivateBookListItemModel) => {
        return {
            type: PrivateBookListActionType.UPDATE_ITEM,
            item
        };
    }),
    switchEditModeForItem: createAction(PrivateBookListActionType.SWITCH_ITEM_EDIT_MODE, (itemId: number) => {
        return {
            type: PrivateBookListActionType.SWITCH_ITEM_EDIT_MODE,
            itemId
        };
    }),
    setBookStatuses: createAction(PrivateBookListActionType.SET_BOOK_STATUSES, (statuses: SelectListItem[]) => {
        return {
            type: PrivateBookListActionType.SET_BOOK_STATUSES,
            statuses
        };
    })
};

const returnOfActions = Object.values(privateBookListAction).map(getReturnOfExpression);
export type PrivateBookListAction = typeof returnOfActions[number];