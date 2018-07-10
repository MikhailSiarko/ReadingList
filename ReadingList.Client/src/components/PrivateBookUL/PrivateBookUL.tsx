import * as React from 'react';
import Fieldset from '../Fieldset';
import styles from './PrivateBookUL.css';
import { createDOMAttributeProps } from '../../utils';

export interface PrivateBookULProps extends React.DOMAttributes<HTMLFieldSetElement> {
    items: JSX.Element[] | undefined;
    legend: string | JSX.Element;
}

const PrivateBookUL: React.StatelessComponent<PrivateBookULProps> = (props) => {
    var fieldsetProps = createDOMAttributeProps(props, 'items', 'legend');
    return (
        <Fieldset className={styles['list-fieldset']} legend={props.legend} {...fieldsetProps}>
            <ul className={styles['book-list']}>
                {props.items ? props.items : <h3>Here are no book yet</h3>}
            </ul>
        </Fieldset>
    );
};

export default PrivateBookUL;