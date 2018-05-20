import * as React from 'react';
import { Component } from 'react-redux';

interface LayoutProps extends React.Props<any> {
    element: string | Component<any>;
    className?: string;
}

const Layout = (props: LayoutProps) => {
    return (
        <props.element className={props.className}>
            {props.children}
        </props.element>
    );
};

export default Layout;