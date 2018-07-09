import * as React from 'react';
import PrimaryButton from '../PrimaryButton';
import RedButton from '../RedButton';

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
                <input name={'list-name'} type={'text'} required={true} defaultValue={this.props.name} />
                <PrimaryButton type={'submit'}>Save</PrimaryButton>
                <RedButton onClick={this.cancelHandler}>Cancel</RedButton>
            </form>
        );
    }
}

export default PrivateListNameEditForm;