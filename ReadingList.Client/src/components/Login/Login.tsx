import * as React from 'react';
import globalStyles from '../../styles/global.css';
import styles from '../AccountForm/AccountForm.css';
import RoundButton from '../RoundButton';

const Login = () => (
    <div>
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
            <RoundButton radius={3} type={'submit'}>✓</RoundButton>
        </div>
    </div>
);

export default Login;