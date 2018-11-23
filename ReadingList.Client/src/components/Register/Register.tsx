import * as React from 'react';
import globalStyles from '../../styles/global.css';
import styles from '../AccountForm/AccountForm.css';
import { applyClasses } from '../../utils';
import RoundButton from '../RoundButton';

interface RegisterProps {
    onConfirmPasswordChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const Register: React.SFC<RegisterProps> = props => (
    <div>
        <h1 className={styles['account-header']}>Register</h1>
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
        <div>
            <input
                className={globalStyles.shadowed}
                type="password"
                name="confirmPassword"
                placeholder="Confirm Password"
                required={true}
                onChange={props.onConfirmPasswordChange}
            />
            <span hidden={true} id={'validation-message'}>Passwords don't confirm</span>
        </div>
        <div className={styles['form-button-wrapper']}>
            <RoundButton
                id={'submit-button'}
                disabled={true}
                radius={3}
                className={applyClasses(globalStyles.disabled, globalStyles.shadowed)}
                type={'submit'}
            >
                ✓
            </RoundButton>
        </div>
    </div>
);

export default Register;
