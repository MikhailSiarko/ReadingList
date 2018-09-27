import * as React from 'react';
import style from './ItemForm.css';
import Fieldset from '../Fieldset';
import PrimaryButton from '../PrimaryButton';
import { PrivateBookListItem } from '../../models';

interface ItemFormProps {
    onSubmit: (item: PrivateBookListItem) => void;
}

class ItemForm extends React.Component<ItemFormProps> {
    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const title = target.elements['title'];
        const author = target.elements['author'];
        const item = {title: title.value, author: author.value} as PrivateBookListItem;
        this.props.onSubmit(item);
        title.value = '';
        author.value = '';
    }

    render() {
        return (
            <Fieldset className={style['form-fieldset']} legend={'Add book'}>
                <form onSubmit={this.submitHandler} className={style['add-item-form']}>
                    <div>
                        <input type="text" required={true} name={'title'} placeholder={'Title'} />
                    </div>
                    <div>
                        <input type="text" required={true} name={'author'} placeholder={'Author'} />
                    </div>
                    <PrimaryButton type={'submit'}>Add</PrimaryButton>
                </form>
            </Fieldset>
        );
    }
}

export default ItemForm;