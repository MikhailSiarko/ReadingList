import * as React from 'react';
import style from './Fieldset.css';

interface Props {
    id?: string;
    className?: string;
    children: React.ReactNode;
    legend: string | JSX.Element;
}

const Fieldset: React.SFC<Props> = props => {
    return (
        <fieldset className={style.fieldset + ` ${props.className}`} id={props.id}>
            <legend>{props.legend}</legend>
            {props.children}
        </fieldset>
    );    
};

export default Fieldset;