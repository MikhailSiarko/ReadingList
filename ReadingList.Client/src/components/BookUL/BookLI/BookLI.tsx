import * as React from 'react';
import styles from './BookLI.css';
import globalStyles from '../../../styles/global.css';
import { BookStatusKey } from '../../../models/BookList/Implementations/BookStatus';
import { isNullOrEmpty } from '../../../utils';
import { ContextMenuProps } from '../../ContextMenu';
import { BookListItem } from '../../../models/BookList/Implementations/BookListItem';
import { cloneDeep } from 'lodash';

interface BookLIProps {
    id?: string;
    listItem: BookListItem;
    shouldStatusSelectorRender: boolean;
    onSave: (item: BookListItem) => void;
    onCancel: (itemId: string) => void;
    options?: JSX.Element[];
    contextMenu?: React.ReactElement<ContextMenuProps>;
}

interface BookListState {
    listItem: BookListItem;
    isTitleValid: boolean;
    isAuthorValid: boolean;
    placeholderMessage: string;
}

class BookLI extends React.Component<BookLIProps, BookListState> {
    constructor(props: BookLIProps) {
        super(props);
        this.state = { 
            listItem: cloneDeep(this.props.listItem), isTitleValid: true, isAuthorValid: true, placeholderMessage: '' };
    }

    inputChangeHandler = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const itemCopy = cloneDeep(this.state.listItem);
        if(event.target.name === 'status') {
            itemCopy[event.target.name] = event.target.value;
        }
        itemCopy.data[event.target.name] = event.target.value;
        this.setState({listItem: itemCopy});
    }

    saveHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        const { title, author } = this.state.listItem.data;
        const isTitleValid = !isNullOrEmpty(title);
        const isAuthorValid = !isNullOrEmpty(author);
        if(isTitleValid && isAuthorValid) {
            const copy = cloneDeep(this.state.listItem);
            copy.isOnEditMode = false;
            this.props.onSave(copy);
        } else {
            this.setState({isAuthorValid: isAuthorValid, isTitleValid: isTitleValid,
                placeholderMessage: 'Cannot be empty'});
        }      
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.setState({listItem: cloneDeep(this.props.listItem)});
        this.props.onCancel(this.props.listItem.id);
    }

    render() {
        if(this.props.listItem.isOnEditMode) {
            return (
                <li className={styles['editing-book-li']}
                        id={this.props.id}>
                    <div className={styles['editable-book-info']}>
                        <div className={styles['editing-book-title']}>
                            <div>
                                <input type="text" placeholder={this.state.placeholderMessage}
                                     onChange={this.inputChangeHandler}
                                    name="title" value={this.state.listItem.data.title}
                                    className={this.state.isTitleValid ? '' : globalStyles['invalid-input']} />
                            </div>by 
                            <div>
                                <input type="text"placeholder={this.state.placeholderMessage}
                                    onChange={this.inputChangeHandler}
                                    name="author" value={this.state.listItem.data.author} />
                            </div>
                        </div>
                    </div>
                {
                    this.props.shouldStatusSelectorRender
                        ?
                        <div className={styles['status']}>
                            <p>Status:</p>
                            <select onChange={this.inputChangeHandler}
                                     name="status" value={this.state.listItem.status}>
                                {this.props.options}
                            </select>
                        </div>
                        : null
                }
                <div>
                    <div>
                        <button className={`${globalStyles.btn} ${globalStyles.primary}`}
                            onClick={this.saveHandler}>Save</button>
                        <button className={`${globalStyles.btn} ${globalStyles.primary}`}
                            onClick={this.cancelHandler}>Cancel</button>
                    </div>
                </div>
            </li>
            );
        }
        return (
            <li className={styles['book-li']} id={this.props.id}>
                    <div className={styles['book-info']}>
                        <h5 className={styles['book-title']}>
                            <q>{this.props.listItem.data.title}</q> by {this.props.listItem.data.author}
                        </h5>
                    </div>
                {
                    this.props.shouldStatusSelectorRender
                        ?
                        <div className={styles['status']}>
                            <p>Status: {BookStatusKey[this.props.listItem.status]}</p>
                        </div>
                        : null
                }
                {
                    this.props.contextMenu
                }
            </li>
        );
    }
}

export default BookLI;