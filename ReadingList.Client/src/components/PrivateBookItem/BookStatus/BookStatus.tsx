import * as React from 'react';
import styles from './BookStatus.scss';

interface Props {
    status: string;
}

const BookStatus: React.SFC<Props> = ({status}) => {
    return (
        <div className={styles['book-status']}>
            <p>Status:</p>
            <br />
            <p>{status}</p>
        </div>
    );
};

export default BookStatus;