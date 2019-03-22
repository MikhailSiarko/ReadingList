import * as React from 'react';
import styles from './BookGridItem.scss';
import globalStyles from '../../styles/global.scss';
import { reduceTags } from '../../utils';
import * as classNames from 'classnames';

interface BookGridItemProps extends React.HTMLProps<HTMLDivElement> {
    bookId: number;
    header: string;
    tags: string[];
    genre: string;
    selected: boolean;
    onItemClick: (bookId: number) => void;
}

class BookGridItem extends React.Component<BookGridItemProps> {
    handleItemClick = (event: React.MouseEvent<HTMLDivElement>) => {
        event.preventDefault();
        this.props.onItemClick(this.props.bookId);
    }

    render() {
        const { header, tags, genre, bookId, onItemClick, ...restOfProps } = this.props;
        const className = classNames({
            [styles['book-grid-item']]: true,
            [globalStyles['inner-shadowed']]: true,
            [styles['selected-book-grid-item']]: this.props.selected
        });
        return (
            <div
                {...restOfProps}
                className={className}
                data-book-id={bookId}
                onClick={this.handleItemClick}
            >
                <h3>{header}</h3>
                <p>{reduceTags(tags)}</p>
                <h4>{genre}</h4>
            </div>
        );
    }
}

export default BookGridItem;