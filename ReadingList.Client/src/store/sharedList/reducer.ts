import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { SharedListAction, sharedListActions } from './actions';
import { List } from '../../models';
import { cloneDeep } from 'lodash';

export function sharedBookListReducer(state: RootState.Shared = initialState.shared, action: SharedListAction) {
    let copy = cloneDeep(state);
    switch (action.type) {
        case getType(sharedListActions.clearShared):
            return initialState.shared;
        case getType(sharedListActions.switchListEditModeSuccess):
        case getType(sharedListActions.switchListSimpleMode):
            if (copy.list) {
                List.switchMode(copy.list);
            }
            return copy;
        case getType(sharedListActions.updateListSuccess):
        case getType(sharedListActions.fetchListSuccess):
            copy.list = action.payload;
            return copy;
        case getType(sharedListActions.fetchListsSuccess):
            copy.lists = action.payload;
            return copy;
        case getType(sharedListActions.addItemSuccess):
            if (copy.list) {
                List.add(copy.list, action.payload);
            }
            return copy;
        case getType(sharedListActions.deleteItemSuccess):
            if (copy.list) {
                List.removeItem(copy.list, action.payload);
            }
            return copy;
        case getType(sharedListActions.fetchModeratorsSuccess):
            copy.moderators = action.payload;
            return copy;
        default:
            return state;
    }
}