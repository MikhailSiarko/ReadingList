import * as React from 'react';
import style from './FixedButton.css';
import RoundButton from '../RoundButton';

interface Props {
    color?: string;
    radius: number;
    onClick?: (event: React.MouseEvent<HTMLButtonElement>) => void;
    wrapperStyle?: React.CSSProperties;
}

const FixedButton: React.SFC<Props> = props => (
    <RoundButton
        radius={props.radius}
        onClick={props.onClick}
        children={props.children}
        color={props.color}
        wrapperClassName={style.wrapper}
        wrapperStyle={props.wrapperStyle}
    />
);

export default FixedButton;