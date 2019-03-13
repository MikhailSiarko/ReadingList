import * as React from 'react';
import styles from './PrivateBookLI.scss';
import { PrivateBookListItem, SelectListItem } from '../../models';
import { applyClasses, createDOMAttributeProps } from '../../utils';
import globalStyles from '../../styles/global.scss';
import { EditButton, DeleteButton } from './Buttons';
import BookInfo from './BookInfo/BookInfo';
import ReadingTime from './ReadingTime';
import BookStatus from './BookStatus';
import Footer from './Footer';
import BookStatusEditor from './BookStatusEditor';
import BookInfoInEditMode from './BookInfoInEditMode';
import { cloneDeep } from 'lodash';

export interface BookListItemProps extends React.HTMLProps<HTMLLIElement> {
    listItem: PrivateBookListItem;
    onSave: (item: PrivateBookListItem) => void;
    onCancel: (itemId: number) => void;
    onEdit: (itemId: number) => void;
    onDelete: (item: PrivateBookListItem) => void;
    statuses: SelectListItem[];
}

class PrivateBookLI extends React.PureComponent<BookListItemProps> {
    onSubmitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const status = target.elements['status'].value;
        const item = cloneDeep(this.props.listItem);
        item.status = status;
        item.isInEditMode = false;
        this.props.onSave(item);
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel(this.props.listItem.id);
    }

    handleEditButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onEdit(this.props.listItem.id);
    }

    handleDeleteButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onDelete(this.props.listItem);
    }

    render() {
        const liProps = createDOMAttributeProps(
            this.props,
            'listItem',
            'onSave',
            'onCancel',
            'options',
            'statuses',
            'onEdit',
            'onDelete'
        );
        if (this.props.listItem.isInEditMode) {
            return (
                <li className={applyClasses(styles['editing-book-li'], globalStyles['inner-shadowed'])} {...liProps}>
                    <form onSubmit={this.onSubmitHandler}>
                        <BookInfoInEditMode title={this.props.listItem.title} author={this.props.listItem.author} />
                        <BookStatusEditor status={this.props.listItem.status} options={this.props.statuses} />
                        <Footer onCancel={this.cancelHandler} />
                    </form>
                </li>
            );
        }

        return (
            <li className={applyClasses(styles['book-li'], globalStyles['inner-shadowed'])} {...liProps}>
                <BookInfo
                    title={this.props.listItem.title}
                    author={this.props.listItem.author}
                    genre={this.props.listItem.genre}
                />
                <ReadingTime readingTimeInSeconds={this.props.listItem.readingTimeInSeconds} />
                <BookStatus status={this.props.listItem.status} statuses={this.props.statuses} />
                <EditButton onClick={this.handleEditButtonClick} />
                <DeleteButton onClick={this.handleDeleteButtonClick} />
            </li>
        );
    }
}

export default PrivateBookLI;