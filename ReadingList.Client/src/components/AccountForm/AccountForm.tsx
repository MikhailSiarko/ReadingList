import * as React from 'react';
import Layout from '../Layout';
import styles from './AccountForm.css';

const AccountForm = (props: React.HTMLProps<any>) => {
    return (
        <Layout element={'form'} className={styles['account-form']} {...props}>
            <h1 className={styles['account-header']}>Welcome to Reading List!</h1>
            {props.children}
        </Layout>
    );
};

export default AccountForm;