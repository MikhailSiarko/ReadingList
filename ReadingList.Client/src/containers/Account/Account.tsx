import * as React from 'react';
import { Route, RouteComponentProps } from 'react-router';
import Register from '../../components/Register';
import Login from '../../components/Login';
import { connect } from 'react-redux';
import { AuthenticationService } from '../../services';
import { Credentials } from '../../store/actions/authentication';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { RequestResult } from '../../models/Request';
import AccountForm from '../../components/AccountForm';
import { BookListService } from '../../services/BookListService';

interface AccountProps extends RouteComponentProps<any> {
    login: (credentials: Credentials) => Promise<void>;
    register: (credentials: Credentials) => Promise<void>;
}

class Account extends React.Component<AccountProps> {
    private static processSubmit(email: string, password: string, action: (credentials: Credentials) => void) {
        if(Account.validateCredentials(email, password)) {
            action(new Credentials(email, password));
        }
    }

    private static validateCredentials(email: string, password: string) {
        return email !== '' || password !== '';
    }
    public submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const email = (form.elements.namedItem('email') as HTMLInputElement).value;
        const password = (form.elements.namedItem('password') as HTMLInputElement).value;
        const confirmPassword = form.elements.namedItem('confirmPassword') as HTMLInputElement;
        if(confirmPassword === null) {
            Account.processSubmit(email, password, this.props.login);
        } else {
            Account.processSubmit(email, password, this.props.register);
        }
    }

    render() {
        const account = (
            <AccountForm onSubmit={this.submitHandler}>
                <Route path="/account/register" component={Register}/>
                <Route path="/account/login" component={Login}/>
            </AccountForm>
        );
        return <div>{account}</div>;
    }
}

function postRequestProcess(result: RequestResult<any>, ownProps?: AccountProps) {
    if(result.isSucceed) {
        if(ownProps) {
            ownProps.history.push('/');
        }
    } else {
        alert(result.errorMessage);
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>, ownProps: AccountProps) {
    const authService = new AuthenticationService();
    return {
        login: async (credentials: Credentials) => {
            const result = await authService.login(dispatch, credentials);
            if(result.isSucceed) {
                const bookService = new BookListService();
                const bookResult = await bookService.getPrivateList(dispatch);
                postRequestProcess(bookResult, ownProps);
            } else {
                postRequestProcess(result, ownProps);
            }
        },
        register: async (credentials: Credentials) => {
            const result = await authService.register(dispatch, credentials);
            if(result.isSucceed) {
                const bookService = new BookListService();
                const bookResult = await bookService.getPrivateList(dispatch);
                postRequestProcess(bookResult, ownProps);
            } else {
                postRequestProcess(result, ownProps);
            }
        }
    };
}

export default connect(null, mapDispatchToProps)(Account);