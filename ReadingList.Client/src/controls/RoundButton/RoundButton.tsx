import * as React from 'react';
import globalStyles from '../../styles/global.scss';
import style from './RoundButton.scss';
import Colors from '../../styles/colors';
import * as classNames from 'classnames';

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
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
        [globalStyles['red']]: buttonColor === Colors.Red,
        [globalStyles['primary']]: buttonColor === Colors.Primary
    });
    return (
        <div style={wrapperStyle} className={wrapperClassName}>
            <button
                {...restOfProps}
                style={{
                    height: radius * 2 + 'vh',
                    lineHeight: radius * 2 + 'vh',
                    width: radius * 2 + 'vh',
                    fontSize: radius + 'vh',
                    borderRadius: radius + 'vh'
                }}
                className={className}
            >
                {props.children}
            </button>
    </div>
    );

};

export default RoundButton;