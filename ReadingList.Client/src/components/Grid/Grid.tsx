import * as React from 'react';
import styles from './Grid.css';

interface Props {
    items?: JSX.Element[];
}

const Grid: React.SFC<Props> = props => {
    return (
        <div className={styles.grid}>
            {props.items}
        </div>
    );
};

export default Grid;