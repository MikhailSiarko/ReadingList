import * as React from 'react';
import { Book } from '../../models';
import styles from './BookSearchItem.scss';
import { reduceTags } from '../../utils';

interface Props {
    book: Book;
}

const BookSearchItem: React.SFC<Props> = props => (
    <div className={styles['book-search-item']}>
        <p>{props.book.title} by {props.book.author}</p>
        <p>{reduceTags(props.book.tags)}</p>
    </div>
);

export default BookSearchItem;