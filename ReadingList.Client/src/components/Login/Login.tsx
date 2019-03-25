import * as React from 'react';
import styles from '../AccountForm/AccountForm.scss';
import { RoundButton } from '../../controls';

interface Props {
    email: string;
    password: string;
    onEmailChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onPasswordChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

class Login extends React.Component<Props> {
    render() {
        return (
            <>
                <h1 className={styles['account-header']}>Login</h1>
                <div className={styles['wrapper']}>
                    <input
                        type="email"
                        name="email"
                        placeholder="Email"
                        required={true}
                        value={this.props.email}
                        onChange={this.props.onEmailChange}
                    />
                </div>
                <div className={styles['wrapper']}>
                    <input
                        type="password"
                        name="password"
                        placeholder="Password"
                        required={true}
                        value={this.props.password}
                        onChange={this.props.onPasswordChange}
                    />
                </div>
                <div className={styles['form-button-wrapper']}>
                    <RoundButton radius={3} type={'submit'} title="Submit"><i className="fas fa-check"/></RoundButton>
                </div>
            </>
        );
    }
}

export default Login;