import * as React from 'react';
import styles from './GridItem.css';
import globalStyles from '../../../styles/global.css';
import { applyClasses, reduceTags, createDOMAttributeProps } from '../../../utils';

export interface GridItemProps extends React.HTMLProps<HTMLDivElement> {
    header: string;
    tags: string[];
    booksCount: number;
}

const GridItem: React.SFC<GridItemProps> = props => {
    const newProps = createDOMAttributeProps(props, 'header', 'tags', 'booksCount');
    return (
        <div
            {...newProps}
            className={applyClasses(styles['grid-item'], globalStyles['inner-shadowed'])}
            onClick={props.onClick}
        >
            <h3>{props.header}</h3>
            <p>{reduceTags(props.tags)}</p>
            <h4>{props.booksCount} book(s)</h4>
        </div>
    );
};

export default GridItem;