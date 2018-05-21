import * as React from 'react';
import { HTMLProps } from 'react';
import { isNullOrEmpty, AppElement } from '../../utils';

interface LayoutProps extends HTMLProps<any> {
    element: AppElement;
}

const Layout = (props: LayoutProps) => {
    if(typeof props.element === 'string' && isNullOrEmpty(props.element as string)) {
        props.element = 'div';
    }
    return (
        <props.element {...props}>
            {props.children}
        </props.element>
    );
};

export default Layout;