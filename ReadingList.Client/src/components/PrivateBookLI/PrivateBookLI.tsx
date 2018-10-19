import * as React from 'react';
import styles from './PrivateBookLI.css';
import { PrivateBookListItem, SelectListItem } from '../../models';
import Button from '../Button';
import { convertSecondsToReadingTime, createDOMAttributeProps, applyClasses } from '../../utils';
import Colors from '../../styles/colors';
import globalStyles from '../../styles/global.css';

export interface BookListItemProps extends React.DOMAttributes<HTMLLIElement> {
    listItem: PrivateBookListItem;
    onSave: (item: PrivateBookListItem) => void;
    onCancel: (itemId: number) => void;
    statuses: SelectListItem[];
}

const Footer: React.SFC<{onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void}> = ({onCancel}) => (
    <div>
        <Button type="submit">Save</Button>
        <Button onClick={onCancel} color={Colors.Red}>Cancel</Button>
    </div>
);

const Input: React.SFC<{value: string, name: string}> = ({value, name}) => (
    <div>
        <input
            className={globalStyles.shadowed}
            type="text"
            required={true}
            name={name}
            defaultValue={value}
        />
    </div>
);

export const BookInfoEditor: React.SFC<{title: string, author: string}> = ({title, author}) => (
    <div className={styles['editable-book-info']}>
        <div className={styles['editing-book-title']}>
            <Input value={title} name={'title'} />by
            <Input value={author} name={'author'} />
        </div>
    </div>
);

const BookStatusEditor: React.SFC<{status: number, options: SelectListItem[]}> = ({status, options}) => (
    <div className={styles['edited-status']}>
        <p>Status:</p>
        <select className={globalStyles.shadowed} name="status" defaultValue={status.toString()}>
            {
                options
                    ? options.map(item =>
                        <option key={item.value} value={item.value}>{item.text}</option>
                    )
                    : <option key={0} value={0} />
            }
        </select>
    </div>
);

const BookStatus: React.SFC<{status: number, statuses: SelectListItem[]}> = ({status, statuses}) => {
    let statusValue = null;
    if(statuses != null) {
        let filtered = statuses.filter(item => item.value === status);
        if(filtered.length > 0) {
            statusValue = filtered[0].text;
        }
    }
    return (
        <div className={styles['status']}>
            <p>Status:</p>
            <br />
            <p>{statusValue}</p>
        </div>
    );
};

const ReadingTime = ({readingTimeInSeconds}: {readingTimeInSeconds: number}) => (
    <div className={styles['reading-time']}>
        <p>
            Reading time:
        </p>
        <br />
        <p>{convertSecondsToReadingTime(readingTimeInSeconds)}</p>
    </div>
);

export const BookInfo = ({title, author}: {title: string, author: string}) => (
    <div className={styles['book-info']}>
        <h5 className={styles['book-title']}>
            <q>{title}</q> by {author}
        </h5>
    </div>
);

class PrivateBookLI extends React.Component<BookListItemProps> {
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
        const liProps = createDOMAttributeProps(this.props, 'listItem', 'onSave', 'onCancel', 'options', 'statuses');
        if(this.props.listItem.isOnEditMode) {
            return (
                <li className={applyClasses(styles['editing-book-li'], globalStyles['inner-shadowed'])} {...liProps}>
                    <form onSubmit={this.onSubmitHandler}>
                        <BookInfoEditor title={this.props.listItem.title} author={this.props.listItem.author} />
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
            </li>
        );
    }
}

export default PrivateBookLI;