import * as React from 'react';
import styles from './Button.css';
import globalStyles from '../../styles/global.css';
import Colors from '../../styles/colors';
import { applyClasses } from '../../utils';

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    color?: Colors;
}

const Button: React.SFC<Props> = props => (
    <button
        {...props}
        className={applyClasses(styles.button, globalStyles['inner-shadowed'])}
        style={{backgroundColor: props.color ? props.color : Colors.Primary}}
    >
        {props.children}
    </button>
);

export default Button;