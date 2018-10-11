import { createAction, getReturnOfExpression } from 'typesafe-actions';
import { PrivateBookListActionType } from './PrivateBookListActionType';
import { PrivateBookListItem, PrivateBookList, SelectListItem } from '../../../models';

export const privateBookListAction = {
    updateList: createAction(PrivateBookListActionType.UPDATE_LIST, (list: PrivateBookList) => {
        return {
            type: PrivateBookListActionType.UPDATE_LIST,
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
    addItem: createAction(PrivateBookListActionType.ADD_ITEM, (listItem: PrivateBookListItem) => {
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
    updateItem: createAction(PrivateBookListActionType.UPDATE_ITEM, (item: PrivateBookListItem) => {
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