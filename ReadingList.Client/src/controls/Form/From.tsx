import * as React from 'react';
import Colors from '../../styles/colors';
import styles from './Form.scss';
import { RoundButton } from '../RoundButton';

interface Props {
    header: string | JSX.Element;
    size?: {
        width: string;
        height: string;
    };
    onSubmit: (event: React.FormEvent<HTMLFormElement>) => void;
    onCancel?: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const Form: React.SFC<Props> = props => (
    <form
        onSubmit={props.onSubmit}
        className={styles['form']}
    >
        <div
            className={styles['lookup']}
            style={props.size ? props.size : undefined}
            onChange={event => console.log(event)}
        >
            <div className={styles.header}>
                <h2>{props.header}</h2>
                <hr />
            </div>
            <div className={styles['lookup-content']}>
                {props.children}
            </div>
            <div className={styles['buttons-wrapper']}>
                <hr />
                <RoundButton radius={3} type="submit" title="Submit">
                    <i className="fas fa-check" />
                </RoundButton>
                <RoundButton
                    radius={3}
                    onClick={props.onCancel}
                    buttonColor={Colors.Red}
                    title="Cancel"
                >
                    <i className="fas fa-times" />
                </RoundButton>
            </div>
        </div>
    </form>
);

export default Form;