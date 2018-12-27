import * as React from 'react';
import styles from './BookInfo.css';

interface Props {
    title: string;
    author: string;
}

const BookInfo: React.SFC<Props> = props => (
    <div className={styles['book-info']}>
        <h5 className={styles['book-title']}>
            <q>{props.title}</q> by {props.author}
        </h5>
    </div>
);

export default BookInfo;