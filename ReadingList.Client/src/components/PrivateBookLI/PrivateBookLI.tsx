import * as React from 'react';
import styles from './PrivateBookLI.css';
import { PrivateBookListItemModel, BookStatus as BookStatusEnum } from '../../models';
import PrimaryButton from '../PrimaryButton';
import RedButton from '../RedButton';
import { convertSecondsToReadingTime, createDOMAttributeProps } from '../../utils';

export interface PrivateBookLIProps extends React.DOMAttributes<HTMLLIElement> {
    listItem: PrivateBookListItemModel;
    onSave: (item: PrivateBookListItemModel) => void;
    onCancel: (itemId: number) => void;
    options: JSX.Element[];
}

const Footer: React.SFC<{onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void}> = ({onCancel}) => (
    <div>
        <div>
            <PrimaryButton type="submit">Save</PrimaryButton>
            <RedButton onClick={onCancel}>Cancel</RedButton>
        </div>
    </div>
);

const Input: React.SFC<{value: string, name: string}> = ({value, name}) => (
    <div>
        <input
            type="text"
            required={true}
            name={name}
            defaultValue={value}
        />
    </div>
);

const BookInfoEditor: React.SFC<{title: string, author: string}> = ({title, author}) => (
    <div className={styles['editable-book-info']}>
        <div className={styles['editing-book-title']}>
            <Input value={title} name={'title'} />by
            <Input value={author} name={'author'} />
        </div>
    </div>
);

const BookStatusEditor: React.SFC<{status: string, options: JSX.Element[]}> = ({status, options}) => (
    <div className={styles['edited-status']}>
        <p>Status:</p>
        <select name="status" defaultValue={status}>
            {options}
        </select>
    </div>
);

const BookStatus = ({status}: {status: string}) => (
    <div className={styles['status']}>
        <p>Status:</p>
        <br />
        <p>{BookStatusEnum[status]}</p>
    </div>
);

const ReadingTime = ({readingTimeInSeconds}: {readingTimeInSeconds: number}) => (
    <div className={styles['reading-time']}>
        <p>
            Reading time:
        </p>
        <br />
        <p>{convertSecondsToReadingTime(readingTimeInSeconds)}</p>
    </div>
);

const BookInfo = ({title, author}: {title: string, author: string}) => (
    <div className={styles['book-info']}>
        <h5 className={styles['book-title']}>
            <q>{title}</q> by {author}
        </h5>
    </div>
);

class PrivateBookLI extends React.Component<PrivateBookLIProps> {
    onSubmitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const title = target.elements['title'].value;
        const author = target.elements['author'].value;
        const status = target.elements['status'].value;
        const item = Object.assign({}, this.props.listItem, {
            title,
            author,
            status,
            isOnEditMode: false
        });
        this.props.onSave(item);
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel(this.props.listItem.id);
    }

    render() {
        const liProps = createDOMAttributeProps(this.props, 'listItem', 'onSave', 'onCancel', 'options');
        if(this.props.listItem.isOnEditMode) {
            return (
                <li className={styles['editing-book-li']} {...liProps}>
                    <form onSubmit={this.onSubmitHandler}>
                        <BookInfoEditor title={this.props.listItem.title} author={this.props.listItem.author} />
                        <BookStatusEditor status={this.props.listItem.status} options={this.props.options} />
                        <Footer onCancel={this.cancelHandler} />
                    </form>
                </li>
            );
        }
        return (
            <li className={styles['book-li']} {...liProps}>
                <BookInfo title={this.props.listItem.title} author={this.props.listItem.author} />
                <ReadingTime readingTimeInSeconds={this.props.listItem.readingTimeInSeconds} />
                <BookStatus status={this.props.listItem.status} />
            </li>
        );
    }
}

export default PrivateBookLI;