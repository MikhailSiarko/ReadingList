import * as React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { RootState } from '../../store/reducers';
import { connect } from 'react-redux';

interface DefaultRouteProps {
    defaultPath: string;
    isAuthenticated: boolean;
}

class DefaultRoute extends React.Component<DefaultRouteProps> {
    render() {
        return (
            <Route exact={true} path="/" render={props => (
                this.props.isAuthenticated
                    ? <Redirect to={{pathname: this.props.defaultPath, state: {from: props.location}}} />
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

export default connect(mapStateToProps)(DefaultRoute);