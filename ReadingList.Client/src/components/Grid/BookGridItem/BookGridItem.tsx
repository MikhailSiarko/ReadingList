import * as React from 'react';
import styles from '../ListGridItem/ListGridItem.css';
import globalStyles from '../../../styles/global.css';
import { applyClasses, reduceTags, createDOMAttributeProps } from '../../../utils';

export interface BookGridItemProps extends React.HTMLProps<HTMLDivElement> {
    bookId: number;
    header: string;
    tags: string[];
    genre: string;
}

const BookGridItem: React.SFC<BookGridItemProps> = props => {
    const newProps = createDOMAttributeProps(props, 'header', 'tags', 'genre', 'bookId');
    return (
        <div
            {...newProps}
            className={applyClasses(styles['grid-item'], globalStyles['inner-shadowed'])}
            data-book-id={props.bookId}
        >
            <h3>{props.header}</h3>
            <p>{reduceTags(props.tags)}</p>
            <h4>{props.genre}</h4>
        </div>
    );
};

export default BookGridItem;