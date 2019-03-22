import * as React from 'react';
import styles from './ListGridItem.scss';
import globalStyles from '../../styles/global.scss';
import { reduceTags } from '../../utils';
import * as classNames from 'classnames';

interface ListGridItemProps extends React.HTMLProps<HTMLDivElement> {
    header: string;
    tags: string[];
    booksCount: number;
}

const ListGridItem: React.SFC<ListGridItemProps> = props => {
    const { header, tags, booksCount, ...restOfProps } = props;
    return (
        <div
            {...restOfProps}
            className={classNames(styles['grid-item'], globalStyles['inner-shadowed'])}
            onClick={props.onClick}
        >
            <h3>{header}</h3>
            <p>{reduceTags(tags)}</p>
            <h4>{booksCount} book(s)</h4>
        </div>
    );
};

export default ListGridItem;