import * as React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { RootState } from 'src/store';

interface DefaultRouteProps {
    defaultPath: string;
    forPath: string;
    isAuthenticated: boolean;
}

class DefaultRoute extends React.Component<DefaultRouteProps> {
    render() {
        return (
            <Route exact={true} path={this.props.forPath} render={
                props => (
                    this.props.isAuthenticated
                        ? <Redirect to={{pathname: this.props.defaultPath, state: {from: props.location}}} />
                        : <Redirect to={{pathname: '/account/login', state: {from: props.location}}} />
                )
            } />
        );
    }
}

function mapStateToProps(state: RootState) {
    return {
        isAuthenticated: state.identity.isAuthenticated
    };
}

export default connect(mapStateToProps)(DefaultRoute);