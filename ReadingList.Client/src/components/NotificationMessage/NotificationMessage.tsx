import * as React from 'react';
import { NotificationType } from '../../models/NotificationType';
import styles from './NotificationMessage.css';
import Colors from '../../styles/colors';

interface Props {
    hidden: boolean;
    type: NotificationType;
    content: String;
}

let notificationMessageColors = {
    [NotificationType.INFO]: Colors.Primary,
    [NotificationType.ERROR]: Colors.Red
};

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
                style={{backgroundColor: notificationMessageColors[this.props.type]}}
                className={styles.notification}
                ref={ref => this.container = ref as HTMLDivElement}
            >
                <h3>{notificationMessageHeaders[this.props.type]}</h3>
                <p>{this.props.content}</p>
            </div>
        );
    }
}

export default NotificationMessage;