import * as React from 'react';
import Spinner from '../components/Spinner';

export function withSpinner<P>(condition: boolean | null | undefined, Child: React.ComponentType<any>) {
    return class extends React.Component<P> {
        render() {
            if(condition) {
                return <Child />;
            }
            return <Spinner />;
        }
    };
}
