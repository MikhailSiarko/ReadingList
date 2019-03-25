import { createAction } from 'typesafe-actions';
import { SharedListItemActionType } from './actionTypes';
import { SharedBookListItem } from '../../../models';

export const sharedItemActions = {
    addItemBegin: createAction(
        SharedListItemActionType.ADD_SHARED_ITEM_BEGIN,
        action => (listId: number, bookId: number) => action({ listId, bookId })
    ),
    addItemSuccess: createAction(
        SharedListItemActionType.ADD_SHARED_ITEM_SUCCESS,
        action => (listItem: SharedBookListItem) => action(listItem)
    ),
    deleteItemBegin: createAction(
        SharedListItemActionType.DELETE_SHARED_ITEM_BEGIN,
        action => (listId: number, itemId: number) => action({ listId, itemId })
    ),
    deleteItemSuccess: createAction(
        SharedListItemActionType.DELETE_SHARED_ITEM_SUCCESS,
        action => (itemId: number) => action(itemId)
    ),
    shareItem: createAction(
        SharedListItemActionType.SHARE_SHARED_ITEM,
        action => (bookId: number, lists: number[]) => action({bookId, lists})
    )
};