import { LoadingActionType } from './LoadingActionType';
import { createAction, getReturnOfExpression } from 'typesafe-actions';

export const loadingActions = {
    start: createAction(LoadingActionType.START_LOADING, () => {
        return {
            type: LoadingActionType.START_LOADING
        };
    }),
    end: createAction(LoadingActionType.END_LOADING, () => {
        return {
            type: LoadingActionType.END_LOADING,
        };
    }),
};

const returnOfActions = Object.values(loadingActions).map(getReturnOfExpression);
export type LoadingAction = typeof returnOfActions[number];