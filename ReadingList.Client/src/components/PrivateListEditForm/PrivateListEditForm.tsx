import * as React from 'react';
import Colors from '../../styles/colors';
import RoundButton from '../RoundButton';
import style from './PrivateListEditor.scss';
import { PrivateListUpdateData } from 'src/models';

interface Props {
    name: string;
    onSave: (data: PrivateListUpdateData) => void;
    onCancel: () => void;
}

class PrivateListEditForm extends React.Component<Props> {
    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const newName = target.elements['list-name'].value;
        this.props.onSave({ name: newName });
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel();
    }

    render() {
        return (
            <form onSubmit={this.submitHandler} className={style['name-form']}>
                <div>
                    <input
                        name={'list-name'}
                        type={'text'}
                        required={true}
                        defaultValue={this.props.name}
                    />
                </div>
                <div>
                    <RoundButton radius={2} type={'submit'}>
                        <i className="fas fa-check" />
                    </RoundButton>
                </div>
                <div>
                    <RoundButton radius={2} onClick={this.cancelHandler} buttonColor={Colors.Red}>
                        <i className="fas fa-times" />
                    </RoundButton>
                </div>
            </form>
        );
    }
}

export default PrivateListEditForm;