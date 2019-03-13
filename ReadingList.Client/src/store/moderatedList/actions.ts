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
    )
};

export type ModeratedListAction = ActionType<typeof moderatedListActions>;