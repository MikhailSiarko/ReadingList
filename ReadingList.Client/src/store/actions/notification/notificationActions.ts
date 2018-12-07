import { NotificationActionType } from './NotificationActionType';
import { createAction, getReturnOfExpression } from 'typesafe-actions';

export const notificationActions = {
    info: createAction(NotificationActionType.INFO, (message: String) => {
        return {
            type: NotificationActionType.INFO,
            message
        };
    }),
    error: createAction(NotificationActionType.ERROR, (message: String) => {
        return {
            type: NotificationActionType.ERROR,
            message
        };
    }),
    hide: createAction(NotificationActionType.HIDE, () => {
        return {
            type: NotificationActionType.HIDE
        };
    })
};

const returnOfActions = Object.values(notificationActions).map(getReturnOfExpression);
export type NotificationAction = typeof returnOfActions[number];