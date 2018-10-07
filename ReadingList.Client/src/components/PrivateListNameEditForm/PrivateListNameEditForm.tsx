import * as React from 'react';
import Button from '../Button';
import Colors from '../../styles/colors';
import globalStyles from '../../styles/global.css';

interface Props {
    name: string;
    onSave: (newName: string) => void;
    onCancel: () => void;
}

class PrivateListNameEditForm extends React.Component<Props> {
    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const newName = target.elements['list-name'].value;
        this.props.onSave(newName);
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel();
    }

    render() {
        return (
            <form onSubmit={this.submitHandler}>
                <input 
                    name={'list-name'} 
                    type={'text'} 
                    className={globalStyles.shadowed} 
                    required={true} 
                    defaultValue={this.props.name} 
                />
                <Button type={'submit'}>Save</Button>
                <Button onClick={this.cancelHandler} color={Colors.Red}>Cancel</Button>
            </form>
        );
    }
}

export default PrivateListNameEditForm;