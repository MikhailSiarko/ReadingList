import * as React from 'react';
import { convertSecondsToReadingTime } from '../../../utils';
import styles from './ReadingTime.css';

interface Props {
    readingTimeInSeconds: number;
}

const ReadingTime: React.SFC<Props> = props => (
    <div className={styles['reading-time']}>
        <p>
            Reading time:
        </p>
        <br />
        <p>{convertSecondsToReadingTime(props.readingTimeInSeconds)}</p>
    </div>
);

export default ReadingTime;