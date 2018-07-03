import * as React from 'react';
import { RootState } from '../../store/reducers';
import { PrivateBookListItemModel, PrivateBookListModel, generateStatusSelectItems, RequestResult } from '../../models';
import PrivateBookLI from '../../components/PrivateBookLI';
import { cloneDeep } from 'lodash';
import { connect, Dispatch } from 'react-redux';
import { privateBookListAction } from '../../store/actions/privateBookList';
import { PrivateBookListService } from '../../services';
import PrimaryButton from '../../components/PrimaryButton';
import RedButton from '../../components/RedButton';
import { isNullOrEmpty } from '../../utils';
import { withContextMenu } from '../../hoc/withContextMenu';
import PrivateBookUL from '../../components/PrivateBookUL';
import ItemForm from '../../components/ItemForm';

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

class PrivateBookList extends React.Component<Props> {
    async componentDidMount() {
        if(!this.props.bookList) {
            await this.props.getPrivateList();
        }
    }

    submitItemChangesHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if(this.props.bookList) {
            const form = event.target as HTMLFormElement;
            const itemIdInput = form.elements.namedItem('item-id') as HTMLInputElement;
            if(itemIdInput) {
                let item = this.props.bookList.items.find(value => value.id.toString() === itemIdInput.value);
                if(item) {
                    let copy = cloneDeep(item);
                    const target = event.target as HTMLFormElement;
                    const title = target.elements['title'].value;
                    const author = target.elements['author'].value;
                    const status = target.elements['status'].value;
                    Object.assign(copy, {title, author, status, isOnEditMode: false});
                    this.props.updateItem(copy);
                }
            }
        }
    }

    cancelItemChangesHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        const button = event.target as HTMLButtonElement;
        const form = button.form;
        if(this.props.bookList && form) {
            const itemIdInput = form.elements.namedItem('item-id') as HTMLInputElement;
            this.props.switchItemEditMode(parseInt(itemIdInput.value, 10));
        }
    }

    submitListNameHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const newName = target.elements['list-name'].value;
        if(!isNullOrEmpty(newName)) {
            this.props.updateListName(newName);
        }
    }

    submitNewItemHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const title = target.elements['title'].value;
        const author = target.elements['author'].value;
        const isTitleValid = !isNullOrEmpty(title);
        const isAuthorValid = !isNullOrEmpty(author);
        if(isTitleValid && isAuthorValid) {
            const item = {title, author} as PrivateBookListItemModel;
            this.props.addItem(item);
        }
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.setState({bookList: cloneDeep(this.props.bookList as PrivateBookListModel)});
        this.props.switchListEditMode();
    }

    render() {
        const statusOptions = generateStatusSelectItems();
        const options = statusOptions.map(item =>
            <option key={item.value} value={item.value}>{item.text}</option>
        );

        let list;
        if(this.props.bookList) {
            let listItems;
            if(this.props.bookList.items.length > 0) {
                listItems = this.props.bookList.items.map(listItem => {
                    const actions = [
                        {onClick: () => this.props.switchItemEditMode(listItem.id), text: 'Edit'},
                        {onClick: () => this.props.removeItem(listItem.id), text: 'Remove'}
                    ];
                    const Contexed = withContextMenu(actions, PrivateBookLI);
                    return (
                        <Contexed
                            key={listItem.id}
                            listItem={listItem}
                            options={options}
                            onSave={this.props.updateItem}
                            onCancel={this.cancelItemChangesHandler}
                            onChangesSubmit={this.submitItemChangesHandler}
                        />
                    );
                });
            }
            const legend = (
                this.props.bookList.isInEditMode ?
                (
                    <form onSubmit={this.submitListNameHandler}>
                        <input name={'list-name'} type={'text'} defaultValue={this.props.bookList.name} />
                        <PrimaryButton type={'submit'}>Save</PrimaryButton>
                        <RedButton onClick={this.cancelHandler}>Cancel</RedButton>
                    </form>
                ) : this.props.bookList.name

            );
            const bookListActions = [{onClick: this.props.switchListEditMode, text: 'Edit list name'}];
            const ContexedList = withContextMenu(bookListActions, PrivateBookUL);
            list = <ContexedList items={listItems} legend={legend} />;
        }
        return (
            <div>
                {
                    this.props.bookList && !this.props.bookList.isInEditMode
                        ? <ItemForm onSubmit={this.submitNewItemHandler} /> : null
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

export default connect(mapStateToProps, mapDispatchToProps)(PrivateBookList);