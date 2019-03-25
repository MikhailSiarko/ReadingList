import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { TagAction, tagActions } from './actions';

export function tagReducer(state: RootState.Tags = initialState.tags, action: TagAction) {
    switch (action.type) {
        case getType(tagActions.fetchTagSuccess):
            return action.payload;
        case getType(tagActions.clearTagsState):
            return initialState.tags;
        default:
            return state;
    }
}