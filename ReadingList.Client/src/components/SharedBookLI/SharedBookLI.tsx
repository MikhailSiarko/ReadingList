import * as React from 'react';
import styles from '../PrivateBookLI/PrivateBookLI.css';
import { SharedBookListItem } from '../../models';
import { applyClasses, createDOMAttributeProps } from '../../utils';
import globalStyles from '../../styles/global.scss';
import BookInfo from '../PrivateBookLI/BookInfo/BookInfo';
import { DeleteButton } from '../PrivateBookLI/Buttons';
import sharedStyles from './SharedBookLI.css';

export interface SharedBookListItemProps extends React.HTMLProps<HTMLLIElement> {
    item: SharedBookListItem;
    onDelete: (item: SharedBookListItem) => Promise<void>;
}

class SharedBookLI extends React.Component<SharedBookListItemProps> {
    handleDeleteButtonClick = async (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        await this.props.onDelete(this.props.item);
    }
    render() {
        const liProps = createDOMAttributeProps(this.props, 'item', 'onSave', 'onCancel', 'onDelete');
        return (
            <li className={applyClasses(styles['book-li'], globalStyles['inner-shadowed'])} {...liProps}>
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