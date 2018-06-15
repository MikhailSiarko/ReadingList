import * as React from 'react';
import style from './ItemForm.css';
import Fieldset from '../Fieldset';
import PrimaryButton from '../PrimaryButton';

interface ItemFormProps {
    onSubmit: (event: React.FormEvent<HTMLFormElement>) => void;
}

const ItemForm: React.SFC<ItemFormProps> = props => {
    return (
        <Fieldset className={style['form-fieldset']} legend={'Add book'}>
            <form onSubmit={props.onSubmit} className={style['add-item-form']}>
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
};

export default ItemForm;