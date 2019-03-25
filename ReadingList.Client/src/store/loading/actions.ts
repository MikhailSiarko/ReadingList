import { LoadingActionType } from './actionTypes';
import { ActionType, createAction } from 'typesafe-actions';

export const loadingActions = {
    start: createAction(LoadingActionType.START_LOADING),
    end: createAction(LoadingActionType.END_LOADING),
};

export type LoadingAction = ActionType<typeof loadingActions>;