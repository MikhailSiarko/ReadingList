import * as React from 'react';
import style from './ItemForm.css';
import globalStyles from 'src/styles/global.css';
import { cloneDeep } from 'lodash';
import { BookListItem } from '../../../models/BookList/Implementations/BookListItem';
import { guid, isNullOrEmpty } from '../../../utils';
import { BookModel } from '../../../models/BookModel';

interface ItemFormProps {
    onSubmit: (bookItem: BookListItem) => void;
}

interface ItemFormState {
    title: string;
    author: string;
    isTitleValid: boolean;
    isAuthorValid: boolean;
}

class ItemForm extends React.Component<ItemFormProps, ItemFormState> {
    constructor(props: ItemFormProps) {
        super(props);
        this.state = { author: '', title: '', isTitleValid: true, isAuthorValid: true };
    }

    changeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        const stateCopy = cloneDeep(this.state);
        stateCopy[event.target.name] = event.target.value;
        this.setState(stateCopy);
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const { title, author } = this.state;
        const isTitleValid = !isNullOrEmpty(title);
        const isAuthorValid = !isNullOrEmpty(author);
        if(isTitleValid && isAuthorValid) {
            const bookModel = {id: guid(), title: this.state.title, author: this.state.author} as BookModel;
            const item = new BookListItem(guid(), bookModel);
            this.props.onSubmit(item);
            this.setState({ author: '', title: '', isAuthorValid: true, isTitleValid: true });
        } else {
            this.setState({ isAuthorValid: isAuthorValid, isTitleValid: isTitleValid });
        }   
        
    }

    render() {
        return (
            <form onSubmit={this.submitHandler} className={style['add-item-form']}>
                <div>
                    <input type="text" required={true} onChange={this.changeHandler} value={this.state.title}
                        name={'title'} placeholder={'Title'} />
                </div>
                <div>
                    <input type="text" required={true} onChange={this.changeHandler} value={this.state.author}
                        name={'author'} placeholder={'Author'} />
                </div>
                <button type={'submit'} className={`${globalStyles.btn} ${globalStyles.primary}`}>Add</button>
            </form>
        );
    }
}

export default ItemForm;