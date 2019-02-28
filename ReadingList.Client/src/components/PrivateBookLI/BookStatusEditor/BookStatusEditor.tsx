import * as React from 'react';
import { SelectListItem } from '../../../models';
import styles from './BookStatusEditor.scss';

interface Props {
    status: number;
    options: SelectListItem[];
}

const BookStatusEditor: React.SFC<Props> = props => (
    <div className={styles['edited-status']}>
        <p>Status:</p>
        <select name="status" defaultValue={props.status.toString()}>
            {
                props.options
                    ? props.options.map(item =>
                        <option key={item.value} value={item.value}>{item.text}</option>
                    )
                    : <option key={0} value={0} />
            }
        </select>
    </div>
);

export default BookStatusEditor;