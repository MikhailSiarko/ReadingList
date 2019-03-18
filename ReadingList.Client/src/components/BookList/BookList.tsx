import * as React from 'react';
import Fieldset from '../Fieldset';
import styles from './BookList.scss';

export interface Props extends React.HTMLProps<HTMLFieldSetElement> {
    items: JSX.Element[] | undefined;
    legend: string | JSX.Element | null;
}

const BookList: React.SFC<Props> = props => {
    const { items, legend, ...restOfProps } = props;
    return (
        <Fieldset className={styles['list-fieldset']} legend={props.legend} {...restOfProps}>
            <ul className={styles['book-list']}>
                {props.items && props.items.length > 0 ? props.items : <h3>Here are no books yet</h3>}
            </ul>
        </Fieldset>
    );
};

export default BookList;