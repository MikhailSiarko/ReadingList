import * as React from 'react';
import { Credentials } from '../../store/actions/authentication';

interface LoginProps {
    login: (credentials: Credentials) => void;
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

    public submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if(this.state.email === '' || this.state.password === '') {
            return;
        }
        const credentials = new Credentials(this.state.email, this.state.password);
        this.props.login(credentials);
    }

    public render() {
        return (
            <form onSubmit={this.submitHandler}>
                <div>
                    <input type="email" name="email" placeholder="Email" required={true}
                           value={this.state.email} onChange={this.changeHandler} />
                </div>
                <div>
                    <input type="password" name="password" placeholder="Password" required={true}
                           value={this.state.password} onChange={this.changeHandler} />
                </div>
                <div>
                    <button className="btn primary" type="submit">Login</button>
                </div>
            </form>
        );
    }
}

export default Login;