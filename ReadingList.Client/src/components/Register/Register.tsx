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
    private submitButton: HTMLButtonElement;

    constructor(props: RegisterProps) {
        super(props);
        this.state = {isPasswordsConfirmed: true};
    }

    validationChangeHandler = () => {
        if(this.password && this.confirmPassword) {
            if(this.confirmPassword.value === this.password.value) {
                if(this.submitButton) {
                    this.setState({isPasswordsConfirmed: true});
                }
            } else {
                if(this.submitButton) {
                    this.setState({isPasswordsConfirmed: false});
                }
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
                           onChange={this.validationChangeHandler}
                           className={this.state.isPasswordsConfirmed ? '' : globalStyles['invalid-input']}/>
                    <span className={this.state.isPasswordsConfirmed ? '' : globalStyles['input-validation-message']}
                          hidden={this.state.isPasswordsConfirmed}>Passwords don't confirm</span>
                </div>
                <div>
                    <PrimaryButton ref={(ref) => this.submitButton = ref as HTMLButtonElement}
                            disabled={!this.state.isPasswordsConfirmed}
                            className={`${this.state.isPasswordsConfirmed ? '' : globalStyles.disabled}`}
                            type="submit">Register</PrimaryButton>
                </div>
            </div>
        );
    }
}

export default Register;
