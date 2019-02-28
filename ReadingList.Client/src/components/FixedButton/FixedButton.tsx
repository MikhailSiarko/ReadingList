import * as React from 'react';
import style from './FixedButton.scss';
import RoundButton from '../RoundButton';
import Colors from '../../styles/colors';

interface Props {
    color?: Colors;
    radius: number;
    onClick?: (event: React.MouseEvent<HTMLButtonElement>) => void;
    wrapperStyle?: React.CSSProperties;
    title?: string;
}

const FixedButton: React.SFC<Props> = props => (
    <RoundButton
        radius={props.radius}
        onClick={props.onClick}
        children={props.children}
        buttonColor={props.color}
        wrapperClassName={style.wrapper}
        wrapperStyle={props.wrapperStyle}
        title={props.title}
    />
);

export default FixedButton;