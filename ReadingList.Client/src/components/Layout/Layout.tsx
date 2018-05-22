import * as React from 'react';
import { isNullOrEmpty } from '../../utils';
import { AppElement } from '../../types';

interface LayoutProps extends React.HTMLProps<any> {
    element: AppElement;
}

const Layout = (props: LayoutProps) => {
    if(typeof props.element === 'string' && isNullOrEmpty(props.element as string)) {
        props.element = 'div';
    }
    let propsCopy = {...props};
    delete propsCopy.element;
    return (
        <props.element {...propsCopy}>
            {props.children}
        </props.element>
    );
};

export default Layout;