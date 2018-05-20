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
import Layout from '../../components/Layout';
import { BookList } from '../../models/BookList/Implementations/BookList';
import { ListType } from '../../models/BookList/Abstractions/ListType';
import { BookListItem } from '../../models/BookList/Implementations/BookListItem';
import { BookModel } from '../../models/BookModel';
import PrivateBookList from '../PrivateBookList/PrivateBookList';
import { BookStatus } from '../../models/BookList/Implementations/BookStatus';
import Main from '../../components/Main';

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
                {text: 'List', href: '/'},
                {text: 'Logout', href: '', action: this.signOutHandler}                
            ]
            : [
                {text: 'Login', href: '/account/login'},
                {text: 'Register', href: '/account/register'}
            ];

        const bookList = {
            id: '1',
            type: ListType.Private,
            items: [
                new BookListItem('2', 
                    {id: '35', title: 'Martin Eden', author: 'Jack London'} as BookModel, BookStatus.Reading),
                new BookListItem('5', {id: '456', title: 'Three comrades', author: 'Erich Maria Remark'} as BookModel)
            ]
        } as BookList;
        return (
            <Layout element={'div'}>
                <NavBar links={navLinks} />
                <Main>
                    <Switch>
                        <PrivateRoute exact={true} path="/"
                            component={() => <PrivateBookList bookList={bookList} />} />
                        <Route path="/account" component={Account} />
                    </Switch>
                </Main>
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