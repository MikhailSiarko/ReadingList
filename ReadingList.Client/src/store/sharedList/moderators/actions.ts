import { createAction } from 'typesafe-actions';
import { ModeratorActionType } from './actionTypes';
import { User } from 'src/models';

export const moderatorActions = {
    fetchModeratorsBegin: createAction(ModeratorActionType.FETCH_MODERATORS_BEGIN),
    fetchModeratorsSuccess: createAction(
        ModeratorActionType.FETCH_MODERATORS_SUCCESS,
        action => (moderators: User[]) => action(moderators)
    )
};