import * as React from 'react';
import styles from './BookStatus.scss';

interface Props {
    status: string;
}

const BookStatus: React.SFC<Props> = ({status}) => {
    return (
        <div className={styles['book-status']}>
            <p className={styles['status-content']}>Status:</p>
            <br />
            <p className={styles['status-content']}>{status}</p>
        </div>
    );
};

export default BookStatus;