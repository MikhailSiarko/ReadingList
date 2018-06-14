import * as React from 'react';
import styles from './PrivateBookLI.css';
import { PrivateBookListItemModel, BookStatus } from '../../models';
import { cloneDeep } from 'lodash';
import PrimaryButton from '../PrimaryButton';
import RedButton from '../RedButton';

export interface PrivateBookLIProps {
    listItem: PrivateBookListItemModel;
    onSave: (item: PrivateBookListItemModel) => void;
    onCancel: (itemId: number) => void;
    options: JSX.Element[];
}

class PrivateBookLI extends React.Component<PrivateBookLIProps> {
    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let copy = cloneDeep(this.props.listItem);
        const target = event.target as HTMLFormElement;
        const title = target.elements['title'].value;
        const author = target.elements['author'].value;
        const status = target.elements['status'].value;
        copy = Object.assign(copy, {title, author, status, isOnEditMode: false});
        this.props.onSave(copy); 
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel(this.props.listItem.id);
    }
    
    convertSecondsToTime(seconds: number): string {
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
                <li className={styles['editing-book-li']} >
                    <form onSubmit={this.submitHandler}>
                        <div className={styles['editable-book-info']}>
                            <div className={styles['editing-book-title']}>
                                <div>
                                    <input type="text" required={true}
                                        name="title" defaultValue={this.props.listItem.title} />
                                </div>by 
                                <div>
                                    <input type="text" required={true}
                                        name="author" defaultValue={this.props.listItem.author} />
                                </div>
                            </div>
                        </div>
                        <div className={styles['edited-status']}>
                            <p>Status:</p>
                            <select name="status" defaultValue={this.props.listItem.status}>
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
            <li className={styles['book-li']}>
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
            </li>
        );
    }
}

export default PrivateBookLI;