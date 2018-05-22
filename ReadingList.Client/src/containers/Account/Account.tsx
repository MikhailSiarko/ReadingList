import * as React from 'react';
import { Route, RouteComponentProps } from 'react-router';
import Register from '../../components/Register';
import Login from '../../components/Login';
import { connect } from 'react-redux';
import { AuthenticationService } from '../../services';
import { Credentials } from '../../store/actions/authentication';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import Layout from '../../components/Layout';
import { RequestResult } from '../../models/Request';
import AccountForm from '../../components/AccountForm';

interface AccountProps extends RouteComponentProps<any> {
    login: (credentials: Credentials) => Promise<void>;
    register: (credentials: Credentials) => Promise<void>;
}

class Account extends React.Component<AccountProps> {
    public submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const email = (form.elements.namedItem('email') as HTMLInputElement).value;
        const password = (form.elements.namedItem('password') as HTMLInputElement).value;
        const confirmPassword = form.elements.namedItem('confirmPassword') as HTMLInputElement;
        if(confirmPassword === null) {
            this.processSubmit(email, password, this.props.login);
        } else {
            this.processSubmit(email, password, this.props.register);
        }
    }

    render() {
        const account = (
            <AccountForm onSubmit={this.submitHandler}>
                <Route path="/account/register" component={Register} />
                <Route path="/account/login" component={Login} />
            </AccountForm>
        );
        return <Layout element={() => account}/>;
    }

    private processSubmit(email: string, password: string, action: (credentials: Credentials) => void) {
        if(this.validateCredentials(email, password)) {
            action(new Credentials(email, password));
        }
    }

    private validateCredentials(email: string, password: string) {
        return email !== '' || password !== '';
    }
}

function postAuthReqeustProcess(result: RequestResult<never>, ownProps: AccountProps) {
    if(result.isSuccess()) {
        ownProps.history.push('/');
    } else {
        alert(result.message);
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>, ownProps: AccountProps) {
    return {
        login: async (credentials: Credentials) => {
            const result = await AuthenticationService.login(dispatch, credentials);
            postAuthReqeustProcess(result, ownProps);
        },
        register: async (credentials: Credentials) => {
            const result = await AuthenticationService.register(dispatch, credentials);
            postAuthReqeustProcess(result, ownProps);
        }
    };
}

export default connect(null, mapDispatchToProps)(Account);