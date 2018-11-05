import * as React from 'react';
import styles from '../PrivateBookLI/PrivateBookLI.css';
import { SharedBookListItem } from '../../models';
import { createDOMAttributeProps, applyClasses } from '../../utils';
import globalStyles from '../../styles/global.css';
import { BookInfo, BookInfoEditor } from '../PrivateBookLI/PrivateBookLI';

export interface SharedBookListItemProps extends React.DOMAttributes<HTMLLIElement> {
    item: SharedBookListItem;
    onSave?: (item: SharedBookListItem) => void;
    onCancel?: (itemId: number) => void;
}

class SharedBookLI extends React.Component<SharedBookListItemProps> {
    onSubmitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const title = target.elements['title'].value;
        const author = target.elements['author'].value;
        const status = target.elements['status'].value;
        const item = Object.assign({}, this.props.item, {
            title,
            author,
            status,
            isOnEditMode: false
        });
        if(this.props.onSave) {
            this.props.onSave(item);
        }
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if(this.props.onCancel) {
            this.props.onCancel(this.props.item.id);
        }
    }

    render() {
        const liProps = createDOMAttributeProps(this.props, 'item', 'onSave', 'onCancel');
        if(this.props.item.isOnEditMode) {
            return (
                <li className={applyClasses(styles['editing-book-li'], globalStyles['inner-shadowed'])} {...liProps}>
                    <form onSubmit={this.onSubmitHandler}>
                        <BookInfoEditor title={this.props.item.title} author={this.props.item.author} />
                    </form>
                </li>
            );
        }

        return (
            <li className={applyClasses(styles['book-li'], globalStyles['inner-shadowed'])} {...liProps}>
                <BookInfo title={this.props.item.title} author={this.props.item.author} />
            </li>
        );
    }
}

export default SharedBookLI;