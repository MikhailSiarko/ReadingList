import * as React from 'react';
import globalStyles from 'src/styles/global.css';
import Button from '../Button';
import Colors from 'src/styles/colors';
import styles from './SharedListForm.css';

interface Props {
    isHidden: boolean;
    onSubmit: (name: string, tags: string[]) => void;
    onCancel: () => void;
}

class SharedListForm extends React.Component<Props> {
    handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const nameInput = form.elements.namedItem('name') as HTMLInputElement;
        const tagsInput = form.elements.namedItem('tags') as HTMLInputElement;
        if(nameInput && tagsInput) {
            this.props.onSubmit(nameInput.value, tagsInput.value.replace(' ', '').split(','));
            nameInput.value = '';
            tagsInput.value = '';
        }
    }

    resetForm = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        const button = event.target as HTMLButtonElement;
        if(button.form) {
            const nameInput = button.form.elements.namedItem('name') as HTMLInputElement;
            const tagsInput = button.form.elements.namedItem('tags') as HTMLInputElement;
            if(nameInput && tagsInput) {
                nameInput.value = '';
                tagsInput.value = '';
            }
        }
        this.props.onCancel();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit} hidden={this.props.isHidden} className={styles['shared-list-form']}>
                <div className={styles['lookup']}>
                    <div>
                        <h2>Create new list</h2>
                    </div>
                    <div>
                        <input
                            className={globalStyles.shadowed}
                            type="text"
                            name="name"
                            placeholder="Name"
                            required={true}
                        />
                    </div>
                    <div>
                        <input
                            className={globalStyles.shadowed}
                            type="text"
                            name="tags"
                            placeholder="Tags"
                            required={true}
                        />
                    </div>
                    <div>
                        <Button type="submit">Save</Button>
                        <Button onClick={this.resetForm} color={Colors.Red}>Cancel</Button>
                    </div>
                </div>
            </form>
        );
    }

}

export default SharedListForm;