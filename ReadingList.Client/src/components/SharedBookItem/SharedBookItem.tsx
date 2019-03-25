import * as React from 'react';
import styles from '../PrivateBookItem/PrivateBookItem.scss';
import { SharedBookListItem } from '../../models';
import globalStyles from '../../styles/global.scss';
import BookInfo from '../PrivateBookItem/BookInfo/BookInfo';
import { DeleteButton } from '../PrivateBookItem/Buttons';
import sharedStyles from './SharedBookItem.scss';
import * as classNames from 'classnames';

export interface SharedBookListItemProps extends React.HTMLProps<HTMLLIElement> {
    item: SharedBookListItem;
    onDelete: (item: SharedBookListItem) => void;
}

class SharedBookLI extends React.Component<SharedBookListItemProps> {
    handleDeleteButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onDelete(this.props.item);
    }
    render() {
        const { item, onDelete, ...restOfProps } = this.props;
        return (
            <li className={classNames(styles['book-item'], globalStyles['inner-shadowed'])} {...restOfProps}>
                <BookInfo
                    title={item.title}
                    author={item.author}
                    className={sharedStyles['shared-book-item']}
                    genre={item.genre}
                />
                <DeleteButton onClick={this.handleDeleteButtonClick} />
            </li>
        );
    }
}

export default SharedBookLI;