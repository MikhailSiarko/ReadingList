import { RootState } from '../index';
import initialState from '../initialState';
import { RequestAction, requestActions } from '../../actions/request';
import { getType } from 'typesafe-actions';

export function requestReducer(state: RootState.LoadingState = initialState.loading, action: RequestAction) {
    switch (action.type) {
        case getType(requestActions.begin):
            console.log(action.info);
            return true;
        case getType(requestActions.success):
            console.log(action.response);
            return false;
        case getType(requestActions.failed):
            console.log(action.response);
            return false;
        default:
            return state;
    }
}