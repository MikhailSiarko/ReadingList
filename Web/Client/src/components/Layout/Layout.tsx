import * as React from 'react';
import { Props } from 'react';

interface LayoutProps extends Props<any> {
    className?: string;
}

const Layout = (props: LayoutProps) => {
    return (
        <div className={props.className}>
            {props.children}
        </div>
    );
};

export default Layout;