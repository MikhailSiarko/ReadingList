import * as React from 'react';
import globalStyles from '../../styles/global.scss';
import style from './RoundButton.scss';
import { applyClasses, createDOMAttributeProps } from '../../utils';
import Colors from '../../styles/colors';

interface Props extends React.HTMLProps<HTMLButtonElement> {
    buttonColor?: Colors;
    radius: number;
    wrapperStyle?: React.CSSProperties;
    wrapperClassName?: string;
}

const RoundButton: React.SFC<Props> = props => {
    const clearProps = createDOMAttributeProps(props, 'buttonColor', 'radius', 'wrapperStyle', 'wrapperClassName');
    return (
        <div style={props.wrapperStyle} className={props.wrapperClassName}>
            <button
                {...clearProps}
                style={{
                    height: props.radius * 2 + 'rem',
                    lineHeight: props.radius * 2 + 'rem',
                    width: props.radius * 2 + 'rem',
                    fontSize: props.radius + 'rem',
                    borderRadius: props.radius + 'rem'
                }}
                className={
                    applyClasses(
                        globalStyles['inner-shadowed'],
                        style['round-button'],
                        props.buttonColor
                            ? props.buttonColor === Colors.Red
                                ? globalStyles.red
                                : globalStyles.primary
                            : globalStyles.primary
                    )
                }
            >
                {props.children}
            </button>
    </div>
    );

};

export default RoundButton;