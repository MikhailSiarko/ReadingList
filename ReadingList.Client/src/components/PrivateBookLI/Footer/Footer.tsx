import * as React from 'react';
import RoundButton from '../../RoundButton';
import Colors from '../../../styles/colors';
import styles from './Footer.css';

interface Props {
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const Footer: React.SFC<Props> = props => (
    <div className={styles['edited-footer']}>
        <RoundButton radius={2} type="submit">✓</RoundButton>
        <RoundButton radius={2} onClick={props.onCancel} color={Colors.Red}>×</RoundButton>
    </div>
);

export default Footer;