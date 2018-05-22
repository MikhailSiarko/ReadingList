import * as React from 'react';
import styles from './AccountForm.css';

const AccountForm = (props: React.HTMLProps<any>) => {
    return (
        <form className={styles['account-form']} {...props}>
            <h1 className={styles['account-header']}>Welcome to Reading List!</h1>
            {props.children}
        </form>
    );
};

export default AccountForm;