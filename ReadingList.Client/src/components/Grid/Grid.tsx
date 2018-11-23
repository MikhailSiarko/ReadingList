import * as React from 'react';
import GridItem, { GridItemProps } from './GridItem';
import styles from './Grid.css';

interface Props {
    items?: GridItemProps[];
    children?: GridItemProps[];
}

const Grid: React.SFC<Props> = props => {
    let items;
    if (props.children) {
        items = (props.children as GridItemProps[]).map(
            (value, index) => (
                <GridItem
                    header={value.header}
                    onClick={value.onClick}
                    content={value.content} key={index}
                />
            ));
    }
    if (props.items) {
        items = props.items.map(
            (value, index) => (
                <GridItem
                    header={value.header}
                    onClick={value.onClick}
                    content={value.content}
                    key={index}
                />
            ));
    }
    return (
        <div className={styles.grid}>
            {items}
        </div>
    );
};

export default Grid;