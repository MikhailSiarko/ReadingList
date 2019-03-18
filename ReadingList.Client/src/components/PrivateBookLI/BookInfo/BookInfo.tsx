import * as React from 'react';
import styles from './BookInfo.scss';
import * as classNames from 'classnames';

interface Props extends React.HTMLProps<HTMLDivElement> {
    title: string;
    author: string;
    genre?: string;
}

const BookInfo: React.SFC<Props> = props => (
    <div className={classNames(styles['book-info'], props.className)}>
        <h5 className={styles['book-title']}>
            <q>{props.title}</q> by {props.author}
        </h5>
    {props.genre && <p className={styles.genre}>({props.genre})</p>}
    </div>
);

export default BookInfo;