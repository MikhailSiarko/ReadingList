import * as React from 'react';
import { Props } from 'react';

interface LayoutProps extends Props<any> {
    tag: string;
    className?: string;
}

const Layout = (props: LayoutProps) => {
    return (
        <props.tag className={props.className}>
            {props.children}
        </props.tag>
    );
};

export default Layout;