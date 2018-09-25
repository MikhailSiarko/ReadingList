import * as React from 'react';
import GridItem, { GridItemProps } from './GridItem';

interface Props {
    items: GridItemProps[];
}

const Grid: React.SFC<Props> = props => {
    var items = props.items.map((item, index) => <GridItem header={item.header} content={item.content} key={index} />);
    return (
        <div>
            {items}
        </div>
    );
};

export default Grid;