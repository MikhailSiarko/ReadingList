import * as React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { RootState } from '../../store/reducers';

interface PrivateRouteProps {
    component: any;
    exact: boolean;
    path: string;
    isAuthenticated: boolean;
}

class PrivateRoute extends React.Component<PrivateRouteProps> {
    render() {
        return (
            <Route exact={this.props.exact} path={this.props.path} render={props => (
                this.props.isAuthenticated
                    ? <this.props.component {...props} />
                    : <Redirect to={{pathname: '/account/login', state: {from: props.location}}} />
            )} />
        );
    }
}

function mapStateToProps(state: RootState) {
    return {
        isAuthenticated: state.identity.isAuthenticated
    };
}

export default connect(mapStateToProps)(PrivateRoute);