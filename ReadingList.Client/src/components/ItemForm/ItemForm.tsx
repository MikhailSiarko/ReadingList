import * as React from 'react';
import style from './ItemForm.css';
import Fieldset from '../Fieldset';
import Button from '../Button';
import { PrivateBookListItem } from '../../models';
import globalStyles from '../../styles/global.css';

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
                        <input 
                            className={globalStyles.shadowed}
                            type="text"
                            required={true} 
                            name={'title'} 
                            placeholder={'Enter a title...'} 
                        />
                    </div>
                    <div>
                        <input 
                            className={globalStyles.shadowed} 
                            type="text" 
                            required={true} 
                            name={'author'} 
                            placeholder={'Enter an author...'}
                        />
                    </div>
                    <Button type={'submit'}>+</Button>
                </form>
            </Fieldset>
        );
    }
}

export default ItemForm;