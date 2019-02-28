import * as React from 'react';
import styles from './BookInfo.scss';
import { applyClasses } from '../../../utils';

interface Props extends React.HTMLProps<HTMLDivElement> {
    title: string;
    author: string;
    genre?: string;
}

const BookInfo: React.SFC<Props> = props => (
    <div className={applyClasses(styles['book-info'], props.className as string)}>
        <h5 className={styles['book-title']}>
            <q>{props.title}</q> by {props.author}
        </h5>
    {props.genre && <p className={styles.genre}>({props.genre})</p>}
    </div>
);

export default BookInfo;