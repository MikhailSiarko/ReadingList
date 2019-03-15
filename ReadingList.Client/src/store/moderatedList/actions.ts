import { ModeratedListActionType } from './actionTypes';
import { ActionType, createAction } from 'typesafe-actions';
import { ListInfo } from 'src/models';

export const moderatedListActions = {
    fetchBegin: createAction(
        ModeratedListActionType.FETCH_MODERATED_LISTS_BEGIN
    ),
    fetchSuccess: createAction(
        ModeratedListActionType.FETCH_MODERATED_LISTS_SUCCESS,
        action => (books: ListInfo[]) => action(books)
    ),
    clearModeratedListsState: createAction(ModeratedListActionType.CLEAR_MODERATED_LISTS_STATE)
};

export type ModeratedListAction = ActionType<typeof moderatedListActions>;