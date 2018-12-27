import * as React from 'react';
import { SelectListItem } from '../../../models';
import globalStyles from '../../../styles/global.css';
import styles from './BookStatusEditor.css';

interface Props {
    status: number;
    options: SelectListItem[];
}

const BookStatusEditor: React.SFC<Props> = props => (
    <div className={styles['edited-status']}>
        <p>Status:</p>
        <select className={globalStyles.shadowed} name="status" defaultValue={status.toString()}>
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