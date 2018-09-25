import * as React from 'react';

export interface GridItemProps {
    header: string;
    content: string;
}

const GridItem: React.SFC<GridItemProps> = props => (
    <div>
        <h3>{props.header}</h3>
        <p>{props.content}</p>
    </div>
);

export default GridItem;