import * as React from 'react';
import styles from './BookInfoInEditMode.scss';

interface Props {
    title: string;
    author: string;
}

const BookInfoInEditMode: React.SFC<Props> = props => (
    <div className={styles['editable-book-info']}>
        <div className={styles['editing-book-title']}>
            <div
                className={styles['title-line']}
            >
                {props.title} <span className={styles['by']}>by</span> {props.author}
            </div>
        </div>
    </div>
);

export default BookInfoInEditMode;