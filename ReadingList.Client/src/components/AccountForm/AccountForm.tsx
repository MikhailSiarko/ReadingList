import * as React from 'react';
import styles from './AccountForm.scss';
import { Route } from 'react-router';
import Register from '../Register';
import Login from '../Login';
import { accountRoutes } from '../../routes';

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
                <Route path={accountRoutes.REGISTER} component={Register} />
                <Route path={accountRoutes.LOGIN} component={Login} />
            </form>
        );
    }

}

export default AccountForm;