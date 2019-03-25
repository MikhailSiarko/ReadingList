import * as React from 'react';
import styles from './PrivateBookItem.scss';
import { PrivateBookListItem, SelectListItem, PrivateItemUpdateData } from '../../models';
import globalStyles from '../../styles/global.scss';
import { EditButton, DeleteButton } from './Buttons';
import { BookInfo } from './BookInfo';
import { ReadingTime } from './ReadingTime';
import { BookStatus } from './BookStatus';
import { Footer } from './Footer';
import { BookStatusEditor } from './BookStatusEditor';
import { BookInfoInEditMode } from './BookInfoInEditMode';
import * as classNames from 'classnames';

interface Props extends React.HTMLProps<HTMLLIElement> {
    listItem: PrivateBookListItem;
    onSave: (itemId: number, data: PrivateItemUpdateData) => void;
    onCancel: (itemId: number) => void;
    onEdit: (itemId: number) => void;
    onDelete: (item: PrivateBookListItem) => void;
    statuses: SelectListItem[] | null;
}

class PrivateBookItem extends React.PureComponent<Props> {
    onSubmitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const status = target.elements['status'].value;
        this.props.onSave(this.props.listItem.id, { status });
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
        const {
            listItem,
            onSave,
            onCancel,
            statuses,
            onEdit,
            onDelete,
            ...restOfProps
        } = this.props;
        if (listItem.isInEditMode && statuses) {
            return (
                <li
                    className={classNames(styles['editing-book-item'], globalStyles['inner-shadowed'])}
                    {...restOfProps}
                >
                    <form onSubmit={this.onSubmitHandler}>
                        <BookInfoInEditMode title={listItem.title} author={listItem.author} />
                        <BookStatusEditor status={listItem.status} options={statuses} />
                        <Footer onCancel={this.cancelHandler} />
                    </form>
                </li>
            );
        }

        return (
            <li className={classNames(styles['book-item'], globalStyles['inner-shadowed'])} {...restOfProps}>
                <BookInfo
                    title={listItem.title}
                    author={listItem.author}
                    genre={listItem.genre}
                />
                <ReadingTime readingTimeInSeconds={listItem.readingTimeInSeconds} />
                {statuses && (<BookStatus status={statuses.filter(item => item.value === listItem.status)[0].text} />)}
                <EditButton onClick={this.handleEditButtonClick} />
                <DeleteButton onClick={this.handleDeleteButtonClick} />
            </li>
        );
    }
}

export default PrivateBookItem;