import * as React from 'react';
import { RootState } from '../../store/reducers';
import { connect } from 'react-redux';
import { Route, RouteComponentProps, Switch } from 'react-router';
import PrivateRoute from '../PrivateRoute';
import Hello from '../../components/Hello/Hello';
import { withRouter } from 'react-router';
import Account from '../Account';
import ApiConfiguration from '../../config/ApiConfiguration';
import NavBar from '../../components/NavBar';
import { Dispatch } from 'redux';
import { authenticationActions } from '../../store/actions/authentication';
import Layout from '../../components/Layout';

interface AppProps extends RouteComponentProps<any> {
    identity: RootState.IdentityState;
    signOut: () => void;
}

class App extends React.Component<AppProps> {
    signOutHandler = (event: React.MouseEvent<HTMLAnchorElement>) => {
        event.preventDefault();
        this.props.signOut();
    }
    render() {
        const navLinks = this.props.identity.isAuthenticated
            ? [{text: 'Logout', href: '', action: this.signOutHandler}]
            : [{text: 'Login', href: ApiConfiguration.login},
                {text: 'Register', href: ApiConfiguration.register}
            ];
        return (
            <Layout className="app" tag={'div'}>
                <NavBar links={navLinks} />
                <Layout className={'main'} tag={'main'}>
                    <Switch>
                        <PrivateRoute exact={true} path="/" 
                            component={() => <Hello name={this.props.identity.user 
                                ? this.props.identity.user.firstname : 'Guest'} />} />
                        <Route path="/account" component={Account} />
                    </Switch>
                </Layout>
            </Layout>
        );
    }
}

function mapStateToProps(state: RootState) {
  return {
    identity: state.identity,
  };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        signOut: () => {
            dispatch(authenticationActions.signOut());
        }
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App));