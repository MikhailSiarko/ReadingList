import * as React from 'react';
import styles from './ListGridItem.css';
import globalStyles from '../../../styles/global.css';
import { applyClasses, reduceTags, createDOMAttributeProps } from '../../../utils';

export interface ListGridItemProps extends React.HTMLProps<HTMLDivElement> {
    header: string;
    tags: string[];
    booksCount: number;
}

const ListGridItem: React.SFC<ListGridItemProps> = props => {
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

export default ListGridItem;