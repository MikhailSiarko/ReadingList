import * as React from 'react';
import styles from './AccountForm.css';
import globalStyles from '../../styles/global.css';
import { Route } from 'react-router';
import Register from '../Register';
import Login from '../Login';

interface Props {
    onSubmit: (email: string, password: string, confirmPassword?: string) => void;
}

class AccountForm extends React.Component<Props> {
    public submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const email = (form.elements.namedItem('email') as HTMLInputElement).value;
        const password = (form.elements.namedItem('password') as HTMLInputElement).value;
        const confirmPassword = form.elements.namedItem('confirmPassword') as HTMLInputElement;
        if(confirmPassword === null) {
            this.props.onSubmit(email, password);
        } else {
            this.props.onSubmit(email, password, confirmPassword.value);
        }
    }

    confirmPasswordChangeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        const confirmPasswordInput = event.target as HTMLInputElement;
        const form = confirmPasswordInput.form as HTMLFormElement;
        const passwordInput = form.elements.namedItem('password') as HTMLInputElement;
        const validationSpan = document.getElementById('validation-message') as HTMLSpanElement;
        if(passwordInput && confirmPasswordInput) {
            const submitButton = document.getElementById('submit-button') as HTMLButtonElement;
            if(confirmPasswordInput.value === passwordInput.value) {
                if(submitButton) {
                    submitButton.disabled = false;
                    submitButton.classList.remove(globalStyles.disabled);
                }
                confirmPasswordInput.classList.remove(globalStyles['invalid-input']);
                validationSpan.classList.remove(globalStyles['input-validation-message']);
            } else {
                if(submitButton) {
                    submitButton.disabled = true;
                    submitButton.classList.add(globalStyles.disabled);
                }
                confirmPasswordInput.classList.add(globalStyles['invalid-input']);
                validationSpan.classList.add(globalStyles['input-validation-message']);
            }
        }
    }

    render() {
        return (
            <form className={styles['account-form']} onSubmit={this.submitHandler}>              
                <Route
                    path="/account/register"
                    component={() => <Register onConfirmPasswordChange={this.confirmPasswordChangeHandler} />}
                 />
                <Route path="/account/login" component={Login} />
            </form>
        );
    }
}

export default AccountForm;