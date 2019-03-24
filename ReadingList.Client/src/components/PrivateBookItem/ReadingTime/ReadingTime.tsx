import * as React from 'react';
import { convertSecondsToReadingTime } from '../../../utils';
import styles from './ReadingTime.scss';

interface Props {
    readingTimeInSeconds: number;
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