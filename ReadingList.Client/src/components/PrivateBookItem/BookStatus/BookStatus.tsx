import * as React from 'react';
import { SelectListItem } from '../../../models';
import styles from './BookStatus.scss';

interface Props {
    status: number;
    statuses: SelectListItem[];
}

const BookStatus: React.SFC<Props> = ({status, statuses}) => {
    let statusValue = null;
    if (statuses != null) {
        let filtered = statuses.filter(item => item.value === status);
        if (filtered.length > 0) {
            statusValue = filtered[0].text;
        }
    }
    return (
        <div className={styles['book-status']}>
            <p>Status:</p>
            <br />
            <p>{statusValue}</p>
        </div>
    );
};

export default BookStatus;