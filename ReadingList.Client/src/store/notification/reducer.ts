import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { NotificationAction, notificationActions } from './actions';
import { NotificationType } from 'src/models';

export function notificationReducer(state: RootState.Notification = initialState.notification,
        action: NotificationAction) {

    let newState: RootState.Notification;
    switch (action.type) {
        case getType(notificationActions.info):
            newState = {
                type: NotificationType.INFO,
                hidden: false,
                message: action.payload
            };
            return newState;
        case getType(notificationActions.error):
            newState = {
                type: NotificationType.ERROR,
                hidden: false,
                message: action.payload
            };
            return newState;
        case getType(notificationActions.hide):
            return {
                ...state,
                hidden: true
            };
        default:
            return state;
    }
}