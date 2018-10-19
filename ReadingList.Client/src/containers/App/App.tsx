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
import SharedBookLists from '../SharedBookLists';
import DefaultRoute from '../DefaultRoute';
import SharedBookList from '../SharedBookList';

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
            ? [
                {text: 'Private List', href: '/private'},
                {text: 'Shared Lists', href: '/shared/search'},
                {text: 'Logout', href: '', action: this.signOutHandler}
            ]
            : [
                {text: 'Login', href: '/account/login'},
                {text: 'Register', href: '/account/register'}
            ];

        return (
            <div>
                <NavBar links={navLinks} />
                <Main>
                    <Switch>
                        <PrivateRoute
                            exact={true}
                            path="/private"
                            component={PrivateBookList}
                        />

                        <PrivateRoute
                            exact={false}
                            path="/shared"
                            component={() =>
                                <div>
                                    <Switch>
                                        <Route
                                            exact={true}
                                            path="/shared/search/:query?"
                                            component={SharedBookLists}
                                        />
                                        <Route
                                            exact={true}
                                            path="/shared/:id"
                                            component={SharedBookList}
                                        />
                                        <DefaultRoute defaultPath="/shared/search" forPath="/shared" />
                                    </Switch>
                                </div>
                            }
                        />

                        <Route path="/account" component={Account} />
                        <DefaultRoute defaultPath="/private" forPath="/" />
                    </Switch>
                </Main>
            </div>
        );
    }
}

function mapStateToProps(state: RootState) {
    return {
        identity: state.identity
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        signOut: () => {
            dispatch(privateBookListAction.unsetPrivate());
            dispatch(authenticationActions.signOut());
        }
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App));