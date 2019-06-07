import * as React from 'react';
import { Route, RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { Credentials } from '../../models';
import { AccountForm, Login, Register } from '../../components';
import { isNullOrEmpty } from '../../utils';
import { authenticationActions, RootState } from '../../store';
import { accountRoutes } from '../../routes';

interface Props extends RouteComponentProps<React.HTMLProps<Props>> {
    loading: boolean;
    signIn: (credentials: Credentials) => void;
}

interface State {
    email: string;
    password: string;
    confirmPassword: string | undefined;
}

class Account extends React.Component<Props, State> {
    static validateCredentials(email: string, password: string, confirmPassword?: string) {
        return isNullOrEmpty(email) || !isNullOrEmpty(password) || !isNullOrEmpty(confirmPassword);
    }

    constructor(props: Props) {
        super(props);
        this.state = {
            email: '',
            password: '',
            confirmPassword: undefined
        };
    }

    renderLogin = () => (
        <Login
            email={this.state.email}
            password={this.state.password}
            onEmailChange={this.handleEmailChange}
            onPasswordChange={this.handlePasswordChange}
        />
    )

    renderRegister = () => (
        <Register
            email={this.state.email}
            password={this.state.password}
            confirmPassword={this.state.confirmPassword}
            onEmailChange={this.handleEmailChange}
            onPasswordChange={this.handlePasswordChange}
            onConfirmPasswordChange={this.handleConfirmPasswordChange}
            isValid={this.state.password === this.state.confirmPassword}
        />
    )

    render() {
        return (
            <AccountForm onSubmit={this.handleSubmit}>
                <Route path={accountRoutes.REGISTER} component={this.renderRegister} />
                <Route path={accountRoutes.LOGIN} component={this.renderLogin} />
            </AccountForm>
        );
    }

    handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        this.submit(
            this.state.email,
            this.state.password,
            this.state.confirmPassword
        );
    }

    submit = (email: string, password: string, confirmPassword?: string) => {
        if (confirmPassword) {
            this.submitRegister(email, password, confirmPassword as string);
        } else {
            this.submitLogin(email, password);
        }
    }

    submitLogin = (email: string, password: string) => {
        if (Account.validateCredentials(email, password)) {
            this.props.signIn(new Credentials(email, password));
        }
    }

    submitRegister = (email: string, password: string, confirmPassword: string) => {
        if (Account.validateCredentials(email, password, confirmPassword)) {
            this.props.signIn(new Credentials(email, password, confirmPassword));
        }
    }

    handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        this.setState({ email: event.target.value });
    }

    handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        this.setState({ password: event.target.value });
    }

    handleConfirmPasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        this.setState({ confirmPassword: event.target.value });
    }
}

function mapStateToProps(state: RootState) {
    return {
        loading: state.loading
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        signIn: (credentials: Credentials) => {
            dispatch(authenticationActions.signInBegin(credentials));
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Account);