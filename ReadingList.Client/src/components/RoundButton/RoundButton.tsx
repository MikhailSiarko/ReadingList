import * as React from 'react';
import globalStyles from '../../styles/global.scss';
import style from './RoundButton.scss';
import Colors from '../../styles/colors';
import * as classNames from 'classnames';

interface Props extends React.HTMLProps<HTMLButtonElement> {
    buttonColor?: Colors;
    radius: number;
    wrapperStyle?: React.CSSProperties;
    wrapperClassName?: string;
}

const RoundButton: React.SFC<Props> = props => {
    const { buttonColor, radius, wrapperStyle, wrapperClassName, ...restOfProps } = props;
    const className = classNames({
        [globalStyles['inner-shadowed']]: true,
        [style['round-button']]: true,
        [globalStyles['red']]: props.buttonColor === Colors.Red,
        [globalStyles['primary']]: props.buttonColor === Colors.Primary
    });
    return (
        <div style={props.wrapperStyle} className={props.wrapperClassName}>
            <button
                {...restOfProps}
                style={{
                    height: props.radius * 2 + 'vh',
                    lineHeight: props.radius * 2 + 'vh',
                    width: props.radius * 2 + 'vh',
                    fontSize: props.radius + 'vh',
                    borderRadius: props.radius + 'vh'
                }}
                className={className}
            >
                {props.children}
            </button>
    </div>
    );

};

export default RoundButton;