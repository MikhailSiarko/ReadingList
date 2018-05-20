import * as React from 'react';
import Layout from '../Layout';
import styles from './BookUL.css';

const BookUL = (props: React.Props<any>) => {
    return (
        <Layout element={'ul'} className={styles['book-list']}>
            {props.children}
        </Layout>
    );
};

export default BookUL;