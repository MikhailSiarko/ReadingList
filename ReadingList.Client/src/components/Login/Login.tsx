import * as React from 'react';
import globalStyles from '../../styles/global.scss';
import styles from '../AccountForm/AccountForm.css';
import RoundButton from '../RoundButton';

const Login = () => (
    <>
        <h1 className={styles['account-header']}>Login</h1>
        <div>
            <input
                className={globalStyles.shadowed}
                type="email"
                name="email"
                placeholder="Email"
                required={true}
            />
        </div>
        <div>
            <input
                className={globalStyles.shadowed}
                type="password"
                name="password"
                placeholder="Password"
                required={true}
            />
        </div>
        <div className={styles['form-button-wrapper']}>
            <RoundButton radius={2.7} type={'submit'} title="Submit"><i className="fas fa-check" /></RoundButton>
        </div>
    </>
);

export default Login;