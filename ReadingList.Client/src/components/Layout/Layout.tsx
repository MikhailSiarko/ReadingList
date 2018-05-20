import * as React from 'react';
import { Component } from 'react-redux';
import { HTMLProps } from 'react';

interface LayoutProps extends HTMLProps<any> {
    element: string | Component<any>;
}

const Layout = (props: LayoutProps) => {
    return (
        <props.element className={props.className}>
            {props.children}
        </props.element>
    );
};

export default Layout;