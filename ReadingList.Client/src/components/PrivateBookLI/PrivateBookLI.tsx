import * as React from 'react';
import styles from './PrivateBookLI.css';
import { PrivateBookListItem, SelectListItem } from '../../models';
import { applyClasses, createDOMAttributeProps } from '../../utils';
import globalStyles from '../../styles/global.css';
import EditButton from './EditButton';
import BookInfo from './BookInfo/BookInfo';
import ReadingTime from './ReadingTime';
import BookStatus from './BookStatus';
import Footer from './Footer';
import BookStatusEditor from './BookStatusEditor';
import BookInfoInEditMode from './BookInfoInEditMode';

export interface BookListItemProps extends React.HTMLProps<HTMLLIElement> {
    listItem: PrivateBookListItem;
    onSave: (item: PrivateBookListItem) => void;
    onCancel: (itemId: number) => void;
    onEditButtonClick: (itemId: number) => void;
    statuses: SelectListItem[];
}

class PrivateBookLI extends React.Component<BookListItemProps> {
    onSubmitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const status = target.elements['status'].value;
        const item = Object.assign({}, this.props.listItem, {
            status,
            isOnEditMode: false
        });
        this.props.onSave(item);
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel(this.props.listItem.id);
    }

    handleEditButtonClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onEditButtonClick(this.props.listItem.id);
    }

    render() {
        const liProps = createDOMAttributeProps(
            this.props,
            'listItem',
            'onSave',
            'onCancel',
            'options',
            'statuses',
            'onEditButtonClick'
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
                <BookInfo title={this.props.listItem.title} author={this.props.listItem.author} />
                <ReadingTime readingTimeInSeconds={this.props.listItem.readingTimeInSeconds} />
                <BookStatus status={this.props.listItem.status} statuses={this.props.statuses} />
                <EditButton onClick={this.handleEditButtonClick} />
            </li>
        );
    }
}

export default PrivateBookLI;