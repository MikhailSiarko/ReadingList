import * as React from 'react';
import styles from '../PrivateBookLI/PrivateBookLI.scss';
import { SharedBookListItem } from '../../models';
import globalStyles from '../../styles/global.scss';
import BookInfo from '../PrivateBookLI/BookInfo/BookInfo';
import { DeleteButton } from '../PrivateBookLI/Buttons';
import sharedStyles from './SharedBookLI.scss';
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
            <li className={classNames(styles['book-li'], globalStyles['inner-shadowed'])} {...restOfProps}>
                <BookInfo
                    title={this.props.item.title}
                    author={this.props.item.author}
                    className={sharedStyles['shared-book-li']}
                    genre={this.props.item.genre}
                />
                <DeleteButton onClick={this.handleDeleteButtonClick} />
            </li>
        );
    }
}

export default SharedBookLI;