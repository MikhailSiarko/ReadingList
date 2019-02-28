import * as React from 'react';
import styles from './Buttons.scss';

interface Props {
    onClick: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

 export const EditButton: React.SFC<Props> = props => {
    return (
        <div className={styles['wrapper']}>
            <button className={styles['button']} onClick={props.onClick} title="Edit">
                <i className="fas fa-edit" />
            </button>
        </div>
    );
};

export const DeleteButton: React.SFC<Props> = props => {
    return (
        <div className={styles['wrapper']}>
            <button className={styles['button']} onClick={props.onClick} title="Delete">
                <i className="fas fa-trash-alt" />
            </button>
        </div>
    );
};