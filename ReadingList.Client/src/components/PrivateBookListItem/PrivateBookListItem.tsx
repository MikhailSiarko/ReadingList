import * as React from 'react';
import styles from './PrivateBookListItem.css';
import { ContextMenuProps } from '../../components/ContextMenu';
import { PrivateBookListItemModel, BookStatus } from '../../models';
import { cloneDeep } from 'lodash';
import PrimaryButton from '../PrimaryButton';
import RedButton from '../RedButton';

interface BookLIProps {
    id?: string;
    listItem: PrivateBookListItemModel;
    onSave: (item: PrivateBookListItemModel) => void;
    onCancel: (itemId: number) => void;
    options?: JSX.Element[];
    contextMenu?: React.ReactElement<ContextMenuProps>;
}

interface BookListState {
    listItem: PrivateBookListItemModel;
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
        itemCopy[event.target.name] = event.target.value;
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
    
    convertSecondsToTime = (seconds: number): string => {
        let hours: any = Math.floor(seconds / 3600);
        let minutes: any = Math.floor((seconds - (hours * 3600)) / 60);
        let sec: any = Math.floor(seconds - (hours * 3600) - (minutes * 60));

        if (hours   < 10) {
            hours = '0' + hours;
        }
        if (minutes < 10) {
            minutes = '0' + minutes;
        }
        if (sec < 10) {
            sec = '0' + sec;
        }
        return hours + ':' + minutes + ':' + sec;
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
                                        name="title" value={this.state.listItem.title} />
                                </div>by 
                                <div>
                                    <input type="text" required={true} onChange={this.changeHandler}
                                        name="author" value={this.state.listItem.author} />
                                </div>
                            </div>
                        </div>
                        <div className={styles['status']}>
                            <p>Status:</p>
                            <select onChange={this.changeHandler}
                                    name="status" value={this.state.listItem.status}>
                                {this.props.options}
                            </select>
                        </div>
                        <div>
                            <div>
                                <PrimaryButton type="submit">Save</PrimaryButton>
                                <RedButton onClick={this.cancelHandler}>Cancel</RedButton>
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
                        <q>{this.props.listItem.title}</q> by {this.props.listItem.author}
                    </h5>
                </div>
                <div className={styles['reading-time']}>
                    <p>
                        Reading time:
                    </p>
                    <br />
                    <p>{this.convertSecondsToTime(this.props.listItem.readingTimeInSeconds)}</p>    
                </div>
                <div className={styles['status']}>
                    <p>Status:</p>
                    <br />
                    <p>{BookStatus[this.props.listItem.status]}</p>
                </div>
                {
                    this.props.contextMenu
                }
            </li>
        );
    }
}

export default BookLI;