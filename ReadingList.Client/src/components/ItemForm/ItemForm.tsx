import * as React from 'react';
import style from './ItemForm.css';
import { cloneDeep } from 'lodash';
import { PrivateBookListItemModel } from '../../models';
import { isNullOrEmpty } from '../../utils';
import Fieldset from '../Fieldset';
import PrimaryButton from '../PrimaryButton';

interface ItemFormProps {
    onSubmit: (bookItem: PrivateBookListItemModel) => void;
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
            const item = {title: this.state.title, author: author} as PrivateBookListItemModel;
            this.props.onSubmit(item);
            this.setState({ author: '', title: '', isAuthorValid: true, isTitleValid: true });
        } else {
            this.setState({ isAuthorValid: isAuthorValid, isTitleValid: isTitleValid });
        }   
        
    }

    render() {
        return (
            <Fieldset className={style['form-fieldset']} legend={'Add book'}>
                <form onSubmit={this.submitHandler} className={style['add-item-form']}>
                    <div>
                        <input type="text" required={true} onChange={this.changeHandler} value={this.state.title}
                            name={'title'} placeholder={'Title'} />
                    </div>
                    <div>
                        <input type="text" required={true} onChange={this.changeHandler} value={this.state.author}
                            name={'author'} placeholder={'Author'} />
                    </div>
                    <PrimaryButton type={'submit'}>Add</PrimaryButton>
                </form>
            </Fieldset>           
        );
    }
}

export default ItemForm;