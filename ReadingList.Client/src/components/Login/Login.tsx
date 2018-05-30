import * as React from 'react';
import PrimaryButton from '../PrimaryButton';

interface LoginProps {
}

export interface LoginState {
    email: string;
    password: string;
}

class Login extends React.Component<LoginProps, LoginState> {
    constructor(props: LoginProps) {
        super(props);
        this.state = {
            email: '',
            password: ''
        };
    }
    public changeHandler = (event: React.FormEvent<HTMLInputElement>) => {
        event.preventDefault();
        const { name, value } = event.target as HTMLInputElement;
        this.setState({
            ...this.state,
            [name]: value,
        } as LoginState);
    }

    public render() {
        return (
            <div>
                <div>
                    <input type="email" name="email" placeholder="Email" required={true}
                           value={this.state.email} onChange={this.changeHandler} />
                </div>
                <div>
                    <input type="password" name="password" placeholder="Password" required={true}
                           value={this.state.password} onChange={this.changeHandler} />
                </div>
                <div>
                    <PrimaryButton type="submit">Login</PrimaryButton>
                </div>
            </div>
        );
    }
}

export default Login;