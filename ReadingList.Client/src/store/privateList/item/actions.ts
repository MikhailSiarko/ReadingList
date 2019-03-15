import { createAction } from 'typesafe-actions';
import { PrivateListItemActionType } from './actionTypes';
import { PrivateBookListItem, PrivateItemUpdateData } from 'src/models';

export const itemActions = {
    addItemBegin: createAction(
        PrivateListItemActionType.ADD_PRIVATE_ITEM_BEGIN,
        action => (bookId: number) => action(bookId)
    ),
    updateItemBegin: createAction(
        PrivateListItemActionType.UPDATE_PRIVATE_ITEM_BEGIN,
        action => (itemId: number, data: PrivateItemUpdateData) => action({ itemId, data })
    ),
    deleteItemBegin: createAction(
        PrivateListItemActionType.DELETE_PRIVATE_ITEM_BEGIN,
        action => (itemId: number) => action(itemId)
    ),
    addItemSuccess: createAction(
        PrivateListItemActionType.ADD_PRIVATE_ITEM_SUCCESS,
        action => (listItem: PrivateBookListItem) => action(listItem)
    ),
    deleteItemSuccess: createAction(
        PrivateListItemActionType.DELETE_PRIVATE_ITEM_SUCCESS,
        action => (itemId: number) => action(itemId)
    ),
    updateItemSuccess: createAction(
        PrivateListItemActionType.UPDATE_PRIVATE_ITEM_SUCCESS,
        action => (item: PrivateBookListItem) => action(item)
    ),
    switchItemEditMode: createAction(
        PrivateListItemActionType.SWITCH_PRIVATE_ITEM_MODE,
        action => (itemId: number) => action(itemId)
    )
};