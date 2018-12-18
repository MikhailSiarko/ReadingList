import * as React from 'react';
import { RootState } from '../../store/reducers';
import { connect } from 'react-redux';
import { Route, RouteComponentProps, Switch, Redirect } from 'react-router';
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
import NotificationMessage from '../../components/NotificationMessage';
import { NotificationType } from '../../models/NotificationType';
import Spinner from '../../components/Spinner';

interface AppProps extends RouteComponentProps<any> {
    loading: boolean;
    identity: RootState.Identity;
    notification: RootState.Notification;
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
            <>
                <NavBar links={navLinks} />
                <Main>
                    <Switch>
                        <PrivateRoute
                            exact={true}
                            path="/private"
                            component={PrivateBookList}
                        />

                        <PrivateRoute
                            exact={true}
                            path="/shared/search/:query?"
                            component={SharedBookLists}
                        />
                        <PrivateRoute
                            exact={true}
                            path="/shared/:id"
                            component={SharedBookList}
                        />
                        {
                            !this.props.identity.isAuthenticated
                                ? <Route path="/account" component={Account} />
                                : <Redirect to="/private" />
                        }
                        <DefaultRoute defaultPath="/private" forPath="/" />
                        <DefaultRoute defaultPath="/shared/search" forPath="/shared" />
                    </Switch>
                </Main>
                <NotificationMessage
                    content={this.props.notification.message}
                    type={this.props.notification.type as NotificationType}
                    hidden={this.props.notification.hidden}
                />
                <Spinner loading={this.props.loading} />
            </>
        );
    }
}

function mapStateToProps(state: RootState) {
    return {
        identity: state.identity,
        notification: state.notification,
        loading: state.loading
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