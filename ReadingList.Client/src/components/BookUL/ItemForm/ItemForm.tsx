import * as React from 'react';
import style from './ItemForm.css';
import globalStyles from 'src/styles/global.css';

interface ItemFormProps {
    onSubmit: (event: React.FormEvent<HTMLFormElement>) => void;
}

class ItemForm extends React.Component<ItemFormProps> {
    render() {
        return (
            <form onSubmit={this.props.onSubmit} className={style['add-item-form']}>
                <div>
                    <input type="text" name={'title'} placeholder={'Title'} />
                </div>
                <div>
                    <input type="text" name={'author'} placeholder={'Author'} />
                </div>
                <button type={'submit'} className={`${globalStyles.btn} ${globalStyles.primary}`}>Add</button>
            </form>
        );
    }
}

export default ItemForm;