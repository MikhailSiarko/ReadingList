import * as React from 'react';
import styles from './AccountForm.css';
import { Route } from 'react-router';
import Register from '../Register';
import Login from '../Login';

interface Props {
    onSubmit: (email: string, password: string, confirmPassword?: string) => void;
}

class AccountForm extends React.Component<Props> {
    handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const email = (form.elements.namedItem('email') as HTMLInputElement).value;
        const password = (form.elements.namedItem('password') as HTMLInputElement).value;
        const confirmPassword = form.elements.namedItem('confirmPassword') as HTMLInputElement;
        this.props.onSubmit(email, password, confirmPassword ? confirmPassword.value : undefined);
    }

    render() {
        return (
            <form className={styles['account-form']} onSubmit={this.handleSubmit}>
                <Route path="/account/register" component={Register} />
                <Route path="/account/login" component={Login} />
            </form>
        );
    }

}

export default AccountForm;