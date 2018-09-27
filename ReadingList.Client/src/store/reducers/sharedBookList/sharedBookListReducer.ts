import { RootState } from '../index';
import initialState from '../initialState';
import { getType } from 'typesafe-actions';
import { cloneDeep } from 'lodash';
import { SharedBookListAction, sharedBookListAction } from '../../actions/sharedBookList/sharedBookListAction';

export function sharedBookListReducer(state: RootState = initialState, action: SharedBookListAction) {
    const copy = cloneDeep(state);
    switch (action.type) {
        case getType(sharedBookListAction.fetchOwnSharedLists):
            return copy;
        default:
            return state;
    }
}