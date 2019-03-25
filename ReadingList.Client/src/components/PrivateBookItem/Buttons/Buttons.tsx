import * as React from 'react';
import styles from './Buttons.scss';
import classnames from 'classnames';

interface Props {
    onClick: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

export const EditButton: React.SFC<Props> = props => {
    return (
        <div className={styles['wrapper']}>
            <button className={styles['button']} onClick={props.onClick} title="Edit">
                <i className={classnames('fas fa-edit', styles['icon'])} />
            </button>
        </div>
    );
};

export const DeleteButton: React.SFC<Props> = props => {
    return (
        <div className={styles['wrapper']}>
            <button className={styles['button']} onClick={props.onClick} title="Delete">
                <i className={classnames('fas fa-trash-alt', styles['icon'])} />
            </button>
        </div>
    );
};