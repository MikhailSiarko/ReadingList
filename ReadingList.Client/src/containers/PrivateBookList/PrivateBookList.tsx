import * as React from 'react';
import styles from './PrivateBookList.css';
import Fieldset from '../../components/Fieldset';
import { RootState } from '../../store/reducers';
import { PrivateBookListItemModel, PrivateBookListModel, generateStatusSelectItems, RequestResult } from '../../models';
import ContextMenu from '../../components/ContextMenu';
import PrivateBookListItem from '../../components/PrivateBookListItem';
import { cloneDeep } from 'lodash';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService } from '../../services';
import ItemForm from '../../components/ItemForm';
import PrimaryButton from '../../components/PrimaryButton';
import RedButton from '../../components/RedButton';
import { isNullOrEmpty } from '../../utils';

interface Props {
    bookList: RootState.PrivateList;
    addItem: (listItem: PrivateBookListItemModel) => Promise<void>;
    updateListName: (name: string) => Promise<void>;
    removeItem: (itemId: number) => Promise<void>;
    updateItem: (item: PrivateBookListItemModel) => Promise<void>;
    switchItemEditMode: (itemId: number) => void;
    switchListEditMode: () => void;
    getPrivateList: () => Promise<void>;
}

class PrivateBookUL extends React.Component<Props> {
    async componentDidMount() {
        if(this.props.bookList == null) {
            await this.props.getPrivateList();
        }
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const newName = target.elements['list-name'].value;
        if(!isNullOrEmpty(newName)) {
            this.props.updateListName(newName);
        }
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.setState({bookList: cloneDeep(this.props.bookList as PrivateBookListModel)});
        this.props.switchListEditMode();
    }

    render() {
        const statusOptions = generateStatusSelectItems();
        const options = statusOptions.map((item) =>
            <option key={item.value} value={item.value}>{item.text}</option>
        );

        let list;
        if(this.props.bookList) {
            let listItems;
            if(this.props.bookList.items.length > 0) {
                listItems = this.props.bookList.items.map((listItem: PrivateBookListItemModel) => {
                    const bookListItemId = 'bookListItem' + listItem.id;
                    const contextMenu = (() => (
                    <ContextMenu rootId={bookListItemId} menuItems={
                            [
                                {onClick: () => this.props.switchItemEditMode(listItem.id), text: 'Edit'},
                                {onClick: () => this.props.removeItem(listItem.id), text: 'Remove'}
                            ]
                        } />))();
                    return (
                        <PrivateBookListItem key={listItem.id} listItem={listItem}
                                options={options} onSave={this.props.updateItem} id={bookListItemId}
                                contextMenu={contextMenu} onCancel={this.props.switchItemEditMode} />
                    );
                });
            } else {
                listItems = <h3>Here are no books yet</h3>;
            }
            const bookListId = 'Private_book_List_' + this.props.bookList.id;
            const bookListContextMenu = (
                <ContextMenu rootId={bookListId} menuItems={[
                    {onClick: this.props.switchListEditMode, text: 'Edit list name'}
                ]} />
            );
            if(this.props.bookList.isInEditMode) {
                const legendForm = (
                    <form onSubmit={this.submitHandler}>
                        <input name={'list-name'} type={'text'} defaultValue={this.props.bookList.name} />
                        <PrimaryButton type={'submit'}>Save</PrimaryButton>
                        <RedButton onClick={this.cancelHandler}>Cancel</RedButton>
                    </form>
                );
                list = (
                    <Fieldset className={styles['list-fieldset']} legend={legendForm}>
                        <ul className={styles['book-list']}>
                            {listItems ? listItems : <h3>Here are no book yet</h3>}
                        </ul>
                    </Fieldset>
                );
            } else {
                list = (
                    <Fieldset id={bookListId} className={styles['list-fieldset']} legend={this.props.bookList.name}>
                        <ul className={styles['book-list']}>
                            {listItems ? listItems : <h3>Here are no book yet</h3>}
                        </ul>
                        {bookListContextMenu}
                    </Fieldset>
                );
            }

        }
        return (
            <div>
                {
                    this.props.bookList && !this.props.bookList.isInEditMode 
                        ? <ItemForm onSubmit={this.props.addItem} /> : null
                }               
                {list}
            </div>
        );
    }
}

function postRequestProcess(result: RequestResult<any>) {
    if(!result.isSucceed) {
        alert(result.errorMessage);
    }
}

function mapStateToProps(state: RootState) {
    return {
        bookList: state.privateList
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const bookService = new PrivateBookListService(dispatch);
    return {
        addItem: async (listItem: PrivateBookListItemModel) => {
            const result = await bookService.addItem(listItem);
            postRequestProcess(result);
        },
        removeItem: async (itemId: number) => {
            const result = await bookService.removeItem(itemId);
            const castedResult = result as RequestResult<any>;
            if(castedResult) {
                postRequestProcess(castedResult);
            }
        },
        updateItem: async (item: PrivateBookListItemModel) => {
            const result = await bookService.updateItem(item);
            postRequestProcess(result);
        },
        switchItemEditMode: (itemId: number) => {
            dispatch(privateBookListAction.switchEditModeForItem(itemId));
        },
        getPrivateList: async () => {
            const result = await bookService.getList();
            postRequestProcess(result);
        },
        switchListEditMode: () => {
            dispatch(privateBookListAction.switchEditModeForList());
        },
        updateListName: async (newName: string) => {
            const result = await bookService.updateListName(newName);
            const castedResult = result as RequestResult<any>;
            if(castedResult) {
                postRequestProcess(castedResult);
            }
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookUL);