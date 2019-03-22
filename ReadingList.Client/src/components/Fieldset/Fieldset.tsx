import * as React from 'react';
import style from './Fieldset.scss';
import classnames from 'classnames';

interface Props extends React.HTMLProps<HTMLFieldSetElement> {
    className?: string;
    legend: string | JSX.Element | null;
}

const Fieldset: React.SFC<Props> = props => {
    const { className, children, legend, ...restOfProps } = props;
    return (
        <fieldset className={classnames(style.fieldset, className)} {...restOfProps}>
            <legend>{legend}</legend>
            {children}
        </fieldset>
    );
};

export default Fieldset;