import * as React from 'react';
import style from './PrimaryButton.css';

const PrimaryButton: React.SFC<React.HTMLProps<HTMLButtonElement>> = (props) => {
    return <button {...props} className={style['primary-btn']}>{props.children}</button>;
};

export default PrimaryButton;