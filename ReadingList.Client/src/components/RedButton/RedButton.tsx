import * as React from 'react';
import style from './RedButton.css';

const RedButton: React.SFC<React.HTMLProps<HTMLButtonElement>> = (props) => {
    return <button {...props} className={style['red-btn']}>{props.children}</button>;
};

export default RedButton;