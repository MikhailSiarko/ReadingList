import { NotificationActionType } from './actionTypes';
import { createAction, ActionType } from 'typesafe-actions';

export const notificationActions = {
    info: createAction(NotificationActionType.INFO,  action => (message: String) => action(message)),
    error: createAction(NotificationActionType.ERROR, action => (message: String) => action(message)),
    hide: createAction(NotificationActionType.HIDE)
};

export type NotificationAction = ActionType<typeof notificationActions>;