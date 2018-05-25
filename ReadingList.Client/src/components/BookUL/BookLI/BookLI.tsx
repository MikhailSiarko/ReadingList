import * as React from 'react';
import styles from './BookLI.css';
import globalStyles from '../../../styles/global.css';
import { BookStatusKey } from '../../../models/BookList/Implementations/BookStatus';
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
}

class BookLI extends React.Component<BookLIProps, BookListState> {
    constructor(props: BookLIProps) {
        super(props);
        this.state = { 
            listItem: cloneDeep(this.props.listItem) };
    }

    changeHandler = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const itemCopy = cloneDeep(this.state.listItem);
        if(event.target.name === 'status') {
            itemCopy[event.target.name] = event.target.value;
        }
        itemCopy.data[event.target.name] = event.target.value;
        this.setState({listItem: itemCopy});
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const copy = cloneDeep(this.state.listItem);
        copy.isOnEditMode = false;
        this.props.onSave(copy); 
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
                    <form onSubmit={this.submitHandler}>
                        <div className={styles['editable-book-info']}>
                            <div className={styles['editing-book-title']}>
                                <div>
                                    <input type="text" required={true} onChange={this.changeHandler}
                                        name="title" value={this.state.listItem.data.title} />
                                </div>by 
                                <div>
                                    <input type="text" required={true} onChange={this.changeHandler}
                                        name="author" value={this.state.listItem.data.author} />
                                </div>
                            </div>
                        </div>
                        {
                            this.props.shouldStatusSelectorRender
                                ?
                                <div className={styles['status']}>
                                    <p>Status:</p>
                                    <select onChange={this.changeHandler}
                                            name="status" value={this.state.listItem.status}>
                                        {this.props.options}
                                    </select>
                                </div>
                                : null
                        }
                        <div>
                            <div>
                                <button type="submit" 
                                    className={`${globalStyles.btn} ${globalStyles.primary}`}>Save</button>
                                <button className={`${globalStyles.btn} ${globalStyles.white}`}
                                    onClick={this.cancelHandler}>Cancel</button>
                            </div>
                        </div>
                    </form>
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