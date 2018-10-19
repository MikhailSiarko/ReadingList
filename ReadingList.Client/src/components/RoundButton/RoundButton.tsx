import * as React from 'react';
import styles from './RoundButton.css';
import Colors from 'src/styles/colors';
import globalStyles from 'src/styles/global.css';

interface Props {
    radius: number;
    onClick?: () => void;
}

const RoundButton: React.SFC<Props> = props => (
    <div className={styles['round-button-wrapper']}>
        <button
            style={{
                backgroundColor: Colors.Primary,
                height: props.radius * 2 + 'rem',
                width: props.radius * 2 + 'rem',
                borderRadius: props.radius + 'rem'
            }}
            className={globalStyles['inner-shadowed']}
            onClick={event => {
                event.preventDefault();
                if(props.onClick) {
                    props.onClick();
                }
            }}
        >
            +
        </button>
    </div>
);

export default RoundButton;