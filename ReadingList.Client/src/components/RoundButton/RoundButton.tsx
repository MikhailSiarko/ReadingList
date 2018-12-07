import * as React from 'react';
import Colors from 'src/styles/colors';
import globalStyles from 'src/styles/global.css';
import style from './RoundButton.css';
import { applyClasses, createDOMAttributeProps } from '../../utils';

interface Props extends React.HTMLProps<HTMLButtonElement> {
    color?: string;
    radius: number;
    wrapperStyle?: React.CSSProperties;
    wrapperClassName?: string;
}

const RoundButton: React.SFC<Props> = props => {
    const clearProps = createDOMAttributeProps(props, 'colors', 'radius', 'wrapperStyle', 'wrapperClassName');
    return (
        <div style={props.wrapperStyle} className={props.wrapperClassName}>
            <button
                {...clearProps}
                style={{
                    backgroundColor: props.color ? props.color : Colors.Primary,
                    height: props.radius * 2 + 'rem',
                    width: props.radius * 2 + 'rem',
                    fontSize: props.radius + 'rem',
                    borderRadius: props.radius + 'rem'
                }}
                className={applyClasses(globalStyles['inner-shadowed'], style['round-button'])}
            >
                {props.children}
            </button>
    </div>
    );

};

export default RoundButton;