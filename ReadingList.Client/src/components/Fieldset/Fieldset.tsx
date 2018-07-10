import * as React from 'react';
import style from './Fieldset.css';
import { createDOMAttributeProps } from '../../utils';

interface Props extends React.DOMAttributes<HTMLFieldSetElement> {
    id?: string;
    className?: string;
    children: React.ReactNode;
    legend: string | JSX.Element;
}

const Fieldset: React.SFC<Props> = props => {
    const fieldsetProps = createDOMAttributeProps(props, 'className', 'children', 'legend');
    return (
        <fieldset className={style.fieldset + ` ${props.className}`} {...fieldsetProps}>
            <legend>{props.legend}</legend>
            {props.children}
        </fieldset>
    );
};

export default Fieldset;