import * as React from 'react';
import globalStyles from '../../styles/global.scss';
import styles from '../AccountForm/AccountForm.scss';
import RoundButton from '../RoundButton';

const Register = () => {
    function handlePasswordChange(event: React.ChangeEvent<HTMLInputElement>) {
        const confirmPasswordInput = event.target as HTMLInputElement;
        const form = confirmPasswordInput.form as HTMLFormElement;
        const passwordInput = form.elements.namedItem('password') as HTMLInputElement;
        const validationSpan = document.getElementById('validation-message') as HTMLSpanElement;
        if (passwordInput && confirmPasswordInput) {
            const submitButton = document.getElementById('submit-button') as HTMLButtonElement;
            if (confirmPasswordInput.value === passwordInput.value) {
                if (submitButton) {
                    submitButton.disabled = false;
                    submitButton.classList.remove(globalStyles.disabled);
                }
                confirmPasswordInput.classList.remove(globalStyles['invalid-input']);
                validationSpan.classList.remove(globalStyles['input-validation-message']);
            } else {
                if (submitButton) {
                    submitButton.disabled = true;
                    submitButton.classList.add(globalStyles.disabled);
                }
                confirmPasswordInput.classList.add(globalStyles['invalid-input']);
                validationSpan.classList.add(globalStyles['input-validation-message']);
            }
        }
    }

    return (
        <>
            <h1 className={styles['account-header']}>Register</h1>
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
            <div>
                <input
                    type="password"
                    name="confirmPassword"
                    placeholder="Confirm Password"
                    required={true}
                    onChange={handlePasswordChange}
                />
                <span hidden={true} id={'validation-message'}>Passwords don't confirm</span>
            </div>
            <div className={styles['form-button-wrapper']}>
                <RoundButton
                    id={'submit-button'}
                    disabled={true}
                    radius={2.7}
                    className={globalStyles.disabled}
                    type={'submit'}
                    title="Submit"
                >
                    <i className="fas fa-check" />
                </RoundButton>
            </div>
        </>
    );
};

export default Register;
