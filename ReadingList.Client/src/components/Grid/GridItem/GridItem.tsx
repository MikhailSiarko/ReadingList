import * as React from 'react';
import styles from './GridItem.css';
import globalStyles from '../../../styles/global.css';
import { applyClasses } from '../../../utils';

export interface GridItemProps {
    header: string;
    content: string | JSX.Element;
    onClick?: () => Promise<void>;
}

const GridItem: React.SFC<GridItemProps> = props => (
    <div className={applyClasses(styles['grid-item'], globalStyles.shadowed)} onClick={props.onClick}>
        <h3>{props.header}</h3>
        <div>{props.content}</div>
    </div>
);

export default GridItem;