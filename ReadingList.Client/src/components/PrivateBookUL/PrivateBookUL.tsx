import * as React from 'react';
import Fieldset from '../Fieldset';
import styles from './PrivateBookUL.css';

export interface PrivateBookULProps {
    items: JSX.Element[] | undefined;
    legend: string | JSX.Element;
}

const PrivateBookUL: React.StatelessComponent<PrivateBookULProps> = (props) => {
    return (
        <Fieldset className={styles['list-fieldset']} legend={props.legend}>
            <ul className={styles['book-list']}>
                {props.items ? props.items : <h3>Here are no book yet</h3>}
            </ul>
        </Fieldset>
    );
};

export default PrivateBookUL;