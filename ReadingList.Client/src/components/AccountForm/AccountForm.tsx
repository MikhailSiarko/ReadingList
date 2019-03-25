import * as React from 'react';
import styles from './AccountForm.scss';

interface Props {
    onSubmit: (event: React.FormEvent<HTMLFormElement>) => void;
    children: React.ReactNode;
}

const AccountForm: React.SFC<Props> = props => (
    <form className={styles['account-form']} onSubmit={props.onSubmit}>
        {props.children}
    </form>
);

export default AccountForm;