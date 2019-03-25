import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { moderatedListActions, ModeratedListAction } from './actions';

export function moderatedListReducer(
    state: RootState.ModeratedLists = initialState.moderatedLists,
    action: ModeratedListAction) {
        switch (action.type) {
            case getType(moderatedListActions.fetchSuccess):
                return action.payload;
            case getType(moderatedListActions.clearModeratedListsState):
                return initialState.moderatedLists;
            default:
                return state;
        }
}