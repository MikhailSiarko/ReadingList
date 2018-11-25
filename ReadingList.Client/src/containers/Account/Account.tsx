import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { AuthenticationService } from '../../services';
import { authenticationActions } from '../../store/actions/authentication';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { RequestResult, AuthenticationData, Credentials } from '../../models';
import AccountForm from '../../components/AccountForm';
import { isNullOrEmpty } from '../../utils';
import { loadingActions } from '../../store/actions/loading';
import { withSpinner } from 'src/hoc';

interface AccountProps extends RouteComponentProps<any> {
    loading: boolean;
    login: (credentials: Credentials) => Promise<void>;
    register: (credentials: Credentials) => Promise<void>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

class Account extends React.Component<AccountProps> {
    private static validateCredentials(email: string, password: string, confirmPassword?: string) {
        return isNullOrEmpty(email) || !isNullOrEmpty(password) || !isNullOrEmpty(confirmPassword);
    }

    public submitHandler = async (email: string, password: string, confirmPassword?: string) => {
        this.props.loadingStart();
        if (confirmPassword) {
            await this.submitRegister(email, password, confirmPassword as string);
        } else {
            await this.submitLogin(email, password);
        }
        this.props.loadingEnd();
    }

    render() {
        const Spinnered = withSpinner(!this.props.loading, () => (
            <div>
                <AccountForm onSubmit={this.submitHandler} />
            </div>
        ));
        return <Spinnered />;
    }

    private async submitLogin(email: string, password: string) {
        if (Account.validateCredentials(email, password)) {
            await this.props.login(new Credentials(email, password));
        }
    }

    private async submitRegister(email: string, password: string, confirmPassword: string) {
        if (Account.validateCredentials(email, password, confirmPassword)) {
            await this.props.register(new Credentials(email, password, confirmPassword));
        }
    }
}

async function postAuthProcess(dispatch: Dispatch<RootState>, result: RequestResult<AuthenticationData>,
                               ownProps: AccountProps) {
    if (result.isSucceed && result.data) {
        dispatch(authenticationActions.signIn(result.data));
        ownProps.history.push('/private');
    } else {
        alert(result.errorMessage);
    }
}

function mapStatetoProps(state: RootState) {
    return {
        loading: state.loading
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>, ownProps: AccountProps) {
    const authService = new AuthenticationService();
    return {
        login: async (credentials: Credentials) => {
            const result = await authService.login(credentials);
            await postAuthProcess(dispatch, result, ownProps);
        },
        register: async (credentials: Credentials) => {
            const result = await authService.register(credentials);
            await postAuthProcess(dispatch, result, ownProps);
        },
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        }
    };
}

export default connect(mapStatetoProps, mapDispatchToProps)(Account);