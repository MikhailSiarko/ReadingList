import * as React from 'react';
import Colors from 'src/styles/colors';
import styles from './AddForm.css';
import RoundButton from '../RoundButton';
import { NamedValue } from '../../models';
import { isNullOrEmpty } from '../../utils';

interface Props {
    header: string | JSX.Element;
    hidden: boolean;
    onSubmit: (values: NamedValue[]) => Promise<void>;
    onCancel?: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

export class AddForm extends React.Component<Props> {
    handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const inputs = Array.from(form.elements).filter(
            item => !isNullOrEmpty(item.getAttribute('name')
        ));
        let values = new Array<NamedValue>(0);
        inputs.forEach(element => {
            if(element.tagName === 'SELECT') {
                const select = element as HTMLSelectElement;
                const selected = Array.from(select.selectedOptions).map(i => JSON.parse(i.value));
                values.push({name: select.name, value: selected});
                select.value = '';
            } else {
                const input = element as HTMLInputElement;
                values.push({name: input.name, value: input.value});
                input.value = '';
            }
        });
        await this.props.onSubmit(values);
    }

    resetForm = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        const button = event.target as HTMLButtonElement;
        if (button.form) {
            const nameInput = button.form.elements.namedItem('name') as HTMLInputElement;
            const tagsInput = button.form.elements.namedItem('tags') as HTMLInputElement;
            if (nameInput && tagsInput) {
                nameInput.value = '';
                tagsInput.value = '';
            }
        }
        if (this.props.onCancel) {
            this.props.onCancel(event);
        }
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit} hidden={this.props.hidden} className={styles['add-form']}>
                <div className={styles['lookup']}>
                    <h2>{this.props.header}</h2>
                    <hr />
                    <div className={styles['lookup-content']}>
                        {this.props.children}
                    </div>
                    <div className={styles['buttons-wrapper']}>
                        <RoundButton radius={3} type="submit" title="Submit">✓</RoundButton>
                        <RoundButton
                            radius={3}
                            onClick={this.resetForm}
                            color={Colors.Red}
                            title="Cancel"
                        >×</RoundButton>
                    </div>
                </div>
            </form>
        );
    }

}