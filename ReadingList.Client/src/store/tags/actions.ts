import { createAction, ActionType } from 'typesafe-actions';
import { TagActionType } from './actionTypes';
import { Tag } from '../../models';

export const tagActions = {
    fetchTagBegin: createAction(TagActionType.FETCH_TAGS_BEGIN),
    fetchTagSuccess: createAction(
        TagActionType.FETCH_TAGS_SUCCESS,
        action => (tags: Tag[]) => action(tags)
    ),
    clearTagsState: createAction(TagActionType.CLEAR_TAGS_STATE)
};

export type TagAction = ActionType<typeof tagActions>;