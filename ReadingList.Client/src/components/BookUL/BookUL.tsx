import * as React from 'react';
import styles from './BookUL.css';
import { HTMLProps } from 'react';

const BookUL = (props: HTMLProps<any>) => {
    return (
        <ul className={styles['book-list']}>
            {props.children}
        </ul>
    );
};

export default BookUL;