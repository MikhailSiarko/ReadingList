import * as React from 'react';
import { Credentials } from '../../store/actions/authentication';

interface RegisterProps {
    register: (credentials: Credentials) => void;
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

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if(this.email && this.password) {
            const credentials = new Credentials(this.email.value, this.password.value);
            this.props.register(credentials);
        }
    }

    render() {
        return (
            <form onSubmit={this.submitHandler}>
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
                           className={this.state.isPasswordsConfirmed ? '' : 'invalid-input'}/>
                    <span className={this.state.isPasswordsConfirmed ? '' : 'input-validation-message'}
                          hidden={this.state.isPasswordsConfirmed}>Passwords don't confirm</span>
                </div>
                <div>
                    <button ref={(ref) => this.submitButton = ref as HTMLButtonElement}
                            disabled={!this.state.isPasswordsConfirmed}
                            className={this.state.isPasswordsConfirmed ? 'btn primary' : 'btn disabled'}
                            type="submit">Register</button>
                </div>
            </form>
        );
    }
}

export default Register;
