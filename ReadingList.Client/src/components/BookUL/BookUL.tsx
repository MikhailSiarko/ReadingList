import * as React from 'react';
import styles from './BookUL.css';

interface Props {
    listName: string;
    children: React.ReactNode;
}

const BookUL: React.SFC<Props> = (props) => {
    return (
        <fieldset className={styles['list-fieldset']}>
            <legend>{props.listName}</legend>
            <ul className={styles['book-list']}>
                {props.children ? props.children : <h3>Here are no book yet</h3>}
            </ul>
        </fieldset>
    );
};

export default BookUL;