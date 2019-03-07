import * as React from 'react';
import styles from '../AccountForm/AccountForm.scss';
import RoundButton from '../RoundButton';

const Login = () => (
    <>
        <h1 className={styles['account-header']}>Login</h1>
        <div>
            <input
                type="email"
                name="email"
                placeholder="Email"
                required={true}
            />
        </div>
        <div>
            <input
                type="password"
                name="password"
                placeholder="Password"
                required={true}
            />
        </div>
        <div className={styles['form-button-wrapper']}>
            <RoundButton radius={3} type={'submit'} title="Submit"><i className="fas fa-check" /></RoundButton>
        </div>
    </>
);

export default Login;