import { createAction, getReturnOfExpression } from 'typesafe-actions';
import { SharedBookListActionType } from './SharedBookListActionType';

export const sharedBookListAction = {
    fetchOwnSharedLists: createAction(SharedBookListActionType.FETCH_OWN_LISTS, () => {
        return {
            type: SharedBookListActionType.FETCH_OWN_LISTS
        };
    })
};

const returnOfActions = Object.values(sharedBookListAction).map(getReturnOfExpression);
export type SharedBookListAction = typeof returnOfActions[number];