import * as React from 'react';
import style from './Fieldset.scss';

interface Props extends React.HTMLProps<HTMLFieldSetElement> {
    className?: string;
    legend: string | JSX.Element | null;
}

const Fieldset: React.SFC<Props> = props => {
    const { className, children, legend, ...restOfProps } = props;
    return (
        <fieldset className={style.fieldset + ` ${props.className}`} {...restOfProps}>
            <legend>{props.legend}</legend>
            {props.children}
        </fieldset>
    );
};

export default Fieldset;