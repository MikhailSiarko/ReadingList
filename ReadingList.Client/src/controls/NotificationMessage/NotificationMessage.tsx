import * as React from 'react';
import { NotificationType } from '../../models';
import styles from './NotificationMessage.scss';
import * as classNames from 'classnames';

interface Props {
    hidden: boolean;
    type: NotificationType;
    content: String;
}

let notificationMessageHeaders = {
    [NotificationType.INFO]: 'Info',
    [NotificationType.ERROR]: 'Error'
};

const NotificationMessage: React.SFC<Props> = props => {
    const className = classNames({
        [styles['notification']]: true,
        [styles['info']]: props.type === NotificationType.INFO,
        [styles['error']]: props.type === NotificationType.ERROR,
        [styles['notification-active']]: !props.hidden
    });
    return (
        <div className={className}>
            <h3>{notificationMessageHeaders[props.type]}</h3>
            <p>{props.content}</p>
        </div>
    );
};

export default NotificationMessage;