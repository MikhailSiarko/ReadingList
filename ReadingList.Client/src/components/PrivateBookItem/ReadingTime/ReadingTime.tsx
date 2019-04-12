import * as React from 'react';
import styles from './ReadingTime.scss';

interface Props {
    readingTimeInSeconds: number;
}

function convertSecondsToReadingTime(seconds: number): string {
    let days: any = Math.floor(seconds / 86400);
    let hours: any = Math.floor((seconds - (days * 86400)) / 3600);
    let minutes: any = Math.floor((seconds - (hours * 3600) - (days * 86400)) / 60);

    if (hours < 10) {
        hours = '0' + hours;
    }
    if (minutes < 10) {
        minutes = '0' + minutes;
    }
    return `days: ${days} | hours: ${hours} | minutes: ${minutes}`;
}

const ReadingTime: React.SFC<Props> = props => (
    <div className={styles['reading-time']}>
        <p className={styles['time-content']}>
            Reading time:
        </p>
        <br />
        <p className={styles['time-content']}>
            {convertSecondsToReadingTime(props.readingTimeInSeconds)}
        </p>
    </div>
);

export default ReadingTime;