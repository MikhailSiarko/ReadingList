import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { LoadingAction, loadingActions } from './actions';

export function loadingReducer(state: RootState.Loading = initialState.loading, action: LoadingAction) {
    switch (action.type) {
        case getType(loadingActions.start):
            return true;
        case getType(loadingActions.end):
            return false;
        default:
            return state;
    }
}