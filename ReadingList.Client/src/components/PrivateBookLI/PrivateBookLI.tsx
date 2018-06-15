import * as React from 'react';
import styles from './PrivateBookLI.css';
import { PrivateBookListItemModel, BookStatus } from '../../models';
import PrimaryButton from '../PrimaryButton';
import RedButton from '../RedButton';
import { convertSecondsToReadingTime } from '../../utils';

export interface PrivateBookLIProps {
    listItem: PrivateBookListItemModel;
    onSave: (item: PrivateBookListItemModel) => void;
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
    options: JSX.Element[];
    onChangesSubmit: (event: React.FormEvent<HTMLFormElement>) => void;
}

const PrivateBookLI: React.SFC<PrivateBookLIProps> = props =>  {   
    if(props.listItem.isOnEditMode) {
        return (
            <li className={styles['editing-book-li']} >
                <form onSubmit={props.onChangesSubmit}>
                    <input type="hidden" name="item-id" value={props.listItem.id} />
                    <div className={styles['editable-book-info']}>
                        <div className={styles['editing-book-title']}>
                            <div>
                                <input type="text" required={true}
                                    name="title" defaultValue={props.listItem.title} />
                            </div>by 
                            <div>
                                <input type="text" required={true}
                                    name="author" defaultValue={props.listItem.author} />
                            </div>
                        </div>
                    </div>
                    <div className={styles['edited-status']}>
                        <p>Status:</p>
                        <select name="status" defaultValue={props.listItem.status}>
                            {props.options}
                        </select>
                    </div>
                    <div>
                        <div>
                            <PrimaryButton type="submit">Save</PrimaryButton>
                            <RedButton onClick={props.onCancel}>Cancel</RedButton>
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
                    <q>{props.listItem.title}</q> by {props.listItem.author}
                </h5>
            </div>
            <div className={styles['reading-time']}>
                <p>
                    Reading time:
                </p>
                <br />
                <p>{convertSecondsToReadingTime(props.listItem.readingTimeInSeconds)}</p>    
            </div>
            <div className={styles['status']}>
                <p>Status:</p>
                <br />
                <p>{BookStatus[props.listItem.status]}</p>
            </div>
        </li>
    );
};

export default PrivateBookLI;