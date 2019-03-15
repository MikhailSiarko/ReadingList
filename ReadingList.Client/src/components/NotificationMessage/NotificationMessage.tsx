import * as React from 'react';
import { NotificationType } from '../../models/notificationType';
import styles from './NotificationMessage.scss';
import { applyClasses } from '../../utils';

interface Props {
    hidden: boolean;
    type: NotificationType;
    content: String;
}

let notificationMessageHeaders = {
    [NotificationType.INFO]: 'Info',
    [NotificationType.ERROR]: 'Error'
};

class NotificationMessage extends React.Component<Props> {
    private container: HTMLDivElement;

    constructor(props: Props) {
        super(props);
    }

    componentDidMount() {
        if(this.props.hidden) {
            this.container.classList.remove(styles['notification-active']);
        } else {
            this.container.classList.add(styles['notification-active']);
        }
    }

    componentDidUpdate() {
        if(this.props.hidden) {
            this.container.classList.remove(styles['notification-active']);
        } else {
            this.container.classList.add(styles['notification-active']);
        }
    }

    render() {
        return (
            <div
                className={
                    applyClasses(
                        styles.notification,
                        this.props.type === NotificationType.INFO ? styles.info : styles.error
                    )
                }
                ref={ref => this.container = ref as HTMLDivElement}
            >
                <h3>{notificationMessageHeaders[this.props.type]}</h3>
                <p>{this.props.content}</p>
            </div>
        );
    }
}

export default NotificationMessage;