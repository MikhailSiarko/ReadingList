import * as React from 'react';
import globalStyles from '../../styles/global.scss';
import styles from '../AccountForm/AccountForm.scss';
import RoundButton from '../RoundButton';
import * as classNames from 'classnames';

interface Props {
    email: string;
    password: string;
    confirmPassword: string | undefined;
    isValid: boolean;
    onEmailChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onPasswordChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onConfirmPasswordChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const Register: React.SFC<Props> = props => (
    <>
        <h1 className={styles['account-header']}>Register</h1>
        <div>
            <input
                type="email"
                name="email"
                placeholder="Email"
                required={true}
                value={props.email}
                onChange={props.onEmailChange}
            />
        </div>
        <div>
            <input
                type="password"
                name="password"
                placeholder="Password"
                required={true}
                value={props.password}
                onChange={props.onPasswordChange}
            />
        </div>
        <div>
            <input
                type="password"
                name="confirmPassword"
                placeholder="Confirm Password"
                required={true}
                value={props.confirmPassword}
                onChange={props.onConfirmPasswordChange}
                className={classNames({
                    [globalStyles['invalid-input']]: props.confirmPassword !== undefined &&!props.isValid
                })}
            />
            {
                props.confirmPassword !== undefined && !props.isValid &&
                    (
                        <span
                            hidden={props.isValid}
                            className={globalStyles['input-validation-message']}
                        >
                            Passwords do not confirm
                        </span>
                    )
            }
        </div>
        <div className={styles['form-button-wrapper']}>
            <RoundButton
                id={'submit-button'}
                disabled={!props.isValid}
                radius={3}
                type={'submit'}
                title="Submit"
            >
                <i className="fas fa-check" />
            </RoundButton>
        </div>
    </>
);

export default Register;
