import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, Switch, Redirect, withRouter, Route } from 'react-router';
import PrivateRoute from '../PrivateRoute';
import { NavBar, Main, NotificationMessage, Spinner } from '../../controls';
import { Dispatch } from 'redux';
import PrivateBookList from '../PrivateBookList';
import SharedBookLists from '../SharedBookLists';
import DefaultRoute from '../DefaultRoute';
import SharedBookList from '../SharedBookList';
import { NotificationType } from '../../models';
import { privateListActions, authenticationActions, RootState } from '../../store';
import { accountRoutes, privateListRoutes, sharedListRoutes } from '../../routes';
import Account from '../Account/Account';

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
                {text: 'Private List', href: privateListRoutes.PRIVATE_LIST},
                {text: 'Shared Lists', href: sharedListRoutes.SHARED_LISTS},
                {text: 'Logout', href: '', action: this.signOutHandler}
            ]
            : [
                {text: 'Login', href: accountRoutes.LOGIN},
                {text: 'Register', href: accountRoutes.REGISTER}
            ];

        return (
            <>
                <NavBar links={navLinks} />
                <Main>
                    <Switch>
                        <PrivateRoute
                            exact={true}
                            path={privateListRoutes.PRIVATE_LIST}
                            component={PrivateBookList}
                        />

                        <PrivateRoute
                            exact={true}
                            path={sharedListRoutes.SHARED_LISTS}
                            component={SharedBookLists}
                        />
                        <PrivateRoute
                            exact={true}
                            path={sharedListRoutes.SHARED_LIST}
                            component={SharedBookList}
                        />
                        {
                            !this.props.identity.isAuthenticated
                                ? <Route path={accountRoutes.ACCOUNT} component={Account} />
                                : <Redirect to={privateListRoutes.PRIVATE_LIST} />
                        }
                        <DefaultRoute defaultPath={privateListRoutes.PRIVATE_LIST} forPath="/" />
                        <DefaultRoute defaultPath={sharedListRoutes.SHARED_LISTS} forPath={sharedListRoutes.SHARED} />
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
            dispatch(privateListActions.unsetPrivate());
            dispatch(authenticationActions.signOut());
        }
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App));