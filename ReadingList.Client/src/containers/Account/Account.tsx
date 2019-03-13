import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { Credentials } from '../../models';
import AccountForm from '../../components/AccountForm';
import { isNullOrEmpty } from '../../utils';
import { authenticationActions, RootState } from 'src/store';

interface Props extends RouteComponentProps<React.HTMLProps<Props>> {
    loading: boolean;
    signIn: (credentials: Credentials) => void;
}

class Account extends React.Component<Props> {
    private static validateCredentials(email: string, password: string, confirmPassword?: string) {
        return isNullOrEmpty(email) || !isNullOrEmpty(password) || !isNullOrEmpty(confirmPassword);
    }

    public submitHandler = (email: string, password: string, confirmPassword?: string) => {
        if (confirmPassword) {
            this.submitRegister(email, password, confirmPassword as string);
        } else {
            this.submitLogin(email, password);
        }
    }

    render() {
        return <AccountForm onSubmit={this.submitHandler} />;
    }

    private submitLogin = (email: string, password: string) => {
        if (Account.validateCredentials(email, password)) {
            this.props.signIn(new Credentials(email, password));
        }
    }

    private submitRegister = (email: string, password: string, confirmPassword: string) => {
        if (Account.validateCredentials(email, password, confirmPassword)) {
            this.props.signIn(new Credentials(email, password, confirmPassword));
        }
    }
}

function mapStatetoProps(state: RootState) {
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

export default connect(mapStatetoProps, mapDispatchToProps)(Account);