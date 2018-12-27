import * as React from 'react';
import styles from './EditButton.css';

interface Props {
    onClick: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const EditButton: React.SFC<Props> = props => {
    return (
        <div className={styles['wrapper']}>
            <button className={styles['edit-button']} onClick={props.onClick} title="Edit">🖉</button>
        </div>
    );
};

export default EditButton;