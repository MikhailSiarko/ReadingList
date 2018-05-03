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

interface AccountProps extends RouteComponentProps<any> {
    login: (credentials: Credentials) => void;
    register: (credentials: Credentials) => void;
}

class Account extends React.Component<AccountProps> {
    render() {
        return (
            <Layout className={'account-form'}>
                <h1 className={'account-header'}>Welcome to Reading List!</h1>
                <Route path="/account/register" component={() => <Register register={this.props.register} />} />
                <Route path="/account/login" component={() => <Login login={this.props.login} />} />
            </Layout>
        );
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>, ownProps: AccountProps) {
    return {
        login: (credentials: Credentials) => {
            AuthenticationService.login(dispatch, credentials)
                .then(() => {
                    ownProps.history.push('/');
                });
        },
        register: (credentials: Credentials) => {
            AuthenticationService.register(dispatch, credentials)
                .then(() => {
                    ownProps.history.push('/');
                });
        }
    };
}

export default connect(null, mapDispatchToProps)(Account);