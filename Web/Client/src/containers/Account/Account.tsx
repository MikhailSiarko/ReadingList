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
import { RequestResult } from '../../store/actions/request';

interface AccountProps extends RouteComponentProps<any> {
    login: (credentials: Credentials) => Promise<void>;
    register: (credentials: Credentials) => Promise<void>;
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