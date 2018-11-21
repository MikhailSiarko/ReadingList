import * as React from 'react';
import Fieldset from '../Fieldset';
import styles from './BookList.css';
import { createDOMAttributeProps } from '../../utils';

export interface Props extends React.DOMAttributes<HTMLFieldSetElement> {
    items: JSX.Element[] | undefined;
    legend: string | JSX.Element | null;
}

const BookList: React.StatelessComponent<Props> = props => {
    const fieldsetProps = createDOMAttributeProps(props, 'items', 'legend');
    return (
        <Fieldset className={styles['list-fieldset']} legend={props.legend} {...fieldsetProps}>
            <ul className={styles['book-list']}>
                {props.items ? props.items : <h3>Here are no book yet</h3>}
            </ul>
        </Fieldset>
    );
};

export default BookList;