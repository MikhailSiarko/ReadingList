import * as React from 'react';
import Colors from '../../styles/colors';
import styles from './Form.scss';
import { RoundButton } from '../RoundButton';
import { Container } from '../Container';

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
                <h2 className={styles['header-content']}>{props.header}</h2>
                <hr className={styles['line']} />
            </div>
            <div className={styles['lookup-content']}>
                <Container className={styles['lookup-container']}>
                    {props.children}
                </Container>
            </div>
            <div className={styles['buttons']}>
                <hr className={styles['line']} />
                <RoundButton
                    wrapperClassName={styles['button']}
                    radius={3}
                    type="submit"
                    title="Submit"
                >
                    <i className="fas fa-check" />
                </RoundButton>
                <RoundButton
                    wrapperClassName={styles['button']}
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