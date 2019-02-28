import * as React from 'react';
import style from './Fieldset.scss';
import { createDOMAttributeProps } from '../../utils';

interface Props extends React.HTMLProps<HTMLFieldSetElement> {
    className?: string;
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