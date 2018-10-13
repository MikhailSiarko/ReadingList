import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { AuthenticationService, PrivateBookListService } from '../../services';
import { Credentials, AuthenticationData, authenticationActions } from '../../store/actions/authentication';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { RequestResult } from '../../models';
import AccountForm from '../../components/AccountForm';
import { isNullOrEmpty } from '../../utils';
import { loadingActions } from '../../store/actions/loading';

interface AccountProps extends RouteComponentProps<any> {
    login: (credentials: Credentials) => Promise<void>;
    register: (credentials: Credentials) => Promise<void>;
}

class Account extends React.Component<AccountProps> {
    private static validateCredentials(email: string, password: string, confirmPassword?: string) {
        return isNullOrEmpty(email) || !isNullOrEmpty(password) || !isNullOrEmpty(confirmPassword);
    }

    public submitHandler = async (email: string, password: string, confirmPassword?: string) => {
        if(confirmPassword) {
            await this.submitRegister(email, password, confirmPassword as string);
        } else {
            await this.submitLogin(email, password);
        }
    }

    render() {
        return (
            <div>
                <AccountForm onSubmit={this.submitHandler} />
            </div>
        );
    }

    private async submitLogin(email: string, password: string) {
        if(Account.validateCredentials(email, password)) {
            await this.props.login(new Credentials(email, password));
        }
    }
    private async submitRegister(email: string, password: string, confirmPassword: string) {
        if(Account.validateCredentials(email, password, confirmPassword)) {
            await this.props.register(new Credentials(email, password, confirmPassword));
        }
    }
}

function postRequestProcess(result: RequestResult<any>, ownProps: AccountProps) {
    if(result.isSucceed) {
        ownProps.history.push('/private');
    } else {
        alert(result.errorMessage);
    }
}

async function postAuthProcess(dispatch: Dispatch<RootState>, result: RequestResult<AuthenticationData>,
        ownProps: AccountProps) {
    if(result.isSucceed && result.data) {
        const bookService = new PrivateBookListService();
        dispatch(authenticationActions.signIn(result.data));
        const bookResult = await bookService.getList();
        postRequestProcess(bookResult, ownProps);
    } else {
        postRequestProcess(result, ownProps);
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>, ownProps: AccountProps) {
    const authService = new AuthenticationService();
    return {
        login: async (credentials: Credentials) => {
            dispatch(loadingActions.start());
            const result = await authService.login(credentials);
            dispatch(loadingActions.end());
            await postAuthProcess(dispatch, result, ownProps);
        },
        register: async (credentials: Credentials) => {
            dispatch(loadingActions.start());
            const result = await authService.register(credentials);
            dispatch(loadingActions.end());
            await postAuthProcess(dispatch, result, ownProps);
        }
    };
}

export default connect(null, mapDispatchToProps)(Account);