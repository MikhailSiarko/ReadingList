import * as React from 'react';
import { RootState } from '../../store/reducers';
import { connect } from 'react-redux';
import { Route, RouteComponentProps, Switch } from 'react-router';
import PrivateRoute from '../PrivateRoute';
import { withRouter } from 'react-router';
import Account from '../Account';
import NavBar from '../../components/NavBar';
import { Dispatch } from 'redux';
import { authenticationActions } from '../../store/actions/authentication';
import Main from '../../components/Main';
import { privateBookListAction } from '../../store/actions/privateBookList';
import PrivateBookList from '../PrivateBookList';
import Spinner from '../../components/Spinner';

interface AppProps extends RouteComponentProps<any> {
    loading: RootState.LoadingState;
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
            ? [
                {text: 'Private List', href: '/'},
                {text: 'Logout', href: '', action: this.signOutHandler}
            ]
            : [
                {text: 'Login', href: '/account/login'},
                {text: 'Register', href: '/account/register'}
            ];

        return (
            <div>
                <Spinner loading={this.props.loading} />
                <NavBar links={navLinks} />
                <Main>
                    <Switch>
                        <PrivateRoute
                            exact={true}
                            path="/"
                            component={PrivateBookList}
                        />
                        <Route path="/account" component={Account} />
                    </Switch>
                </Main>
            </div>
        );
    }
}

function mapStateToProps(state: RootState) {
    return {
        loading: state.loading,
        identity: state.identity
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        signOut: () => {
            dispatch(privateBookListAction.unsetPrivateList());
            dispatch(authenticationActions.signOut());
        }
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App));