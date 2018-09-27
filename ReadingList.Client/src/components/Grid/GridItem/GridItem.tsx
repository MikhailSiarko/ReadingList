import * as React from 'react';
import styles from './GridItem.css';

export interface GridItemProps {
    header: string;
    content: string;
    onClick?: () => Promise<void>;
}

const GridItem: React.SFC<GridItemProps> = props => (
    <div className={styles['grid-item']} onClick={props.onClick}>
        <h3>{props.header}</h3>
        <p>{props.content}</p>
    </div>
);

export default GridItem;