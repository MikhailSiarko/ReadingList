import * as React from 'react';
import styles from './BookUL.css';

const BookUL: React.SFC<React.HTMLProps<HTMLUListElement>> = (props) => {
    return (
        <ul className={styles['book-list']}>
            {props.children}
        </ul>
    );
};

export default BookUL;