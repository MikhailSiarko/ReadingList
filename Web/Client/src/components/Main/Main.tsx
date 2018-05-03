import * as React from 'react';
import { Props } from 'react';

interface MainProps extends Props<any> {
    className?: string;
}

const Main = (props: MainProps) => {
    return (
        <main className={props.className}>
            {props.children}
        </main>
    );
};

export default Main;