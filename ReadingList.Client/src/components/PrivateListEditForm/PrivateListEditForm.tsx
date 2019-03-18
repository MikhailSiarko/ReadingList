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

interface State {
    name: string;
}

class PrivateListEditForm extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = { name: props.name };
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        this.props.onSave({ name: this.state.name });
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel();
    }

    handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        this.setState({ name: event.target.value });
    }

    render() {
        return (
            <form onSubmit={this.submitHandler} className={style['name-form']}>
                <div>
                    <input
                        name={'list-name'}
                        type={'text'}
                        required={true}
                        value={this.props.name}
                        onChange={this.handleNameChange}
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