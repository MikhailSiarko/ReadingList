import * as React from 'react';
import RoundButton from '../../RoundButton';
import Colors from '../../../styles/colors';
import styles from './Footer.scss';

interface Props {
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const Footer: React.SFC<Props> = props => (
    <div className={styles['edited-footer']}>
        <RoundButton radius={2} type="submit">
            <i className="fas fa-check" />
        </RoundButton>
        <RoundButton radius={2} onClick={props.onCancel} buttonColor={Colors.Red}>
            <i className="fas fa-times" />
        </RoundButton>
    </div>
);

export default Footer;