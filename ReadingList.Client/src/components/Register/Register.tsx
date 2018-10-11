import * as React from 'react';
import globalStyles from '../../styles/global.css';
import styles from '../AccountForm/AccountForm.css';
import Button from '../Button';
import { applyClasses } from '../../utils';

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
        <div>
            <Button
                id={'submit-button'}
                disabled={true}
                className={applyClasses(globalStyles.disabled, globalStyles.shadowed)}
                type={'submit'}
            >
                Register
            </Button>
        </div>
    </div>
);

export default Register;
