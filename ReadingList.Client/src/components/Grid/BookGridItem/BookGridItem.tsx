import * as React from 'react';
import styles from './BookGridItem.scss';
import globalStyles from '../../../styles/global.scss';
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
            className={applyClasses(styles['book-grid-item'], globalStyles['inner-shadowed'])}
            data-book-id={props.bookId}
        >
            <h3>{props.header}</h3>
            <p>{reduceTags(props.tags)}</p>
            <h4>{props.genre}</h4>
        </div>
    );
};

export default BookGridItem;