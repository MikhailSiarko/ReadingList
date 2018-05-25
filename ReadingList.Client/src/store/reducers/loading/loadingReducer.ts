import { RootState } from '../index';
import initialState from '../initialState';
import { getType } from 'typesafe-actions';
import { LoadingAction, loadingActions } from '../../actions/loading';

export function loadingReducer(state: RootState.LoadingState = initialState.loading, action: LoadingAction) {
    switch (action.type) {
        case getType(loadingActions.start):
            return true;
        case getType(loadingActions.end):
            return false;
        default:
            return state;
    }
}