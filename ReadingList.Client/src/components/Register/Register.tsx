import * as React from 'react';
import globalStyles from '../../styles/global.css';
import PrimaryButton from '../PrimaryButton';

interface RegisterProps {
}

interface RegisterState {
    isPasswordsConfirmed: boolean;
}

class Register extends React.Component<RegisterProps, RegisterState> {
    private password: HTMLInputElement;
    private confirmPassword: HTMLInputElement;
    private email: HTMLInputElement;
    private vaidationSpan: HTMLSpanElement;

    constructor(props: RegisterProps) {
        super(props);
        this.state = {isPasswordsConfirmed: true};
    }

    validationChangeHandler = () => {
        if(this.password && this.confirmPassword) {
            const submitButton = document.getElementById('submit-button');
            if(this.confirmPassword.value === this.password.value) {
                if(submitButton) {
                    submitButton.classList.remove(globalStyles.disabled);
                }
                this.setState({isPasswordsConfirmed: true});
                this.confirmPassword.classList.remove(globalStyles['invalid-input']);
                this.vaidationSpan.classList.remove(globalStyles['input-validation-message']);
            } else {
                if(submitButton) {
                    submitButton.classList.add(globalStyles.disabled);
                }
                this.setState({isPasswordsConfirmed: false});
                this.confirmPassword.classList.add(globalStyles['invalid-input']);
                this.vaidationSpan.classList.add(globalStyles['input-validation-message']);
            }
        }
    }

    render() {
        return (
            <div>
                <div>
                    <input type="email" name="email" placeholder="Email" required={true}
                           ref={(ref) => this.email = ref as HTMLInputElement} />
                </div>
                <div>
                    <input type="password" name="password" placeholder="Password" required={true}
                           ref={(ref) => this.password = ref as HTMLInputElement} />
                </div>
                <div>
                    <input type="password" name="confirmPassword" placeholder="Confirm Password" required={true}
                           ref={(ref) => this.confirmPassword = ref as HTMLInputElement}
                           onChange={this.validationChangeHandler} />
                    <span ref={span => this.vaidationSpan = span as HTMLSpanElement}
                          hidden={this.state.isPasswordsConfirmed}>Passwords don't confirm</span>
                </div>
                <div>
                    <PrimaryButton id={'submit-button'} disabled={!this.state.isPasswordsConfirmed}
                            className={`${this.state.isPasswordsConfirmed ? '' : globalStyles.disabled}`}
                            type="submit">Register</PrimaryButton>
                </div>
            </div>
        );
    }
}

export default Register;
