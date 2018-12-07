import * as React from 'react';
import globalStyles from 'src/styles/global.css';
import Colors from 'src/styles/colors';
import styles from './AddForm.css';
import RoundButton from '../RoundButton';

export type NamedValue = {
    name: string;
    value: any;
};

interface Props {
    header: string | JSX.Element;
    inputs: { type: string, name: string, placeholder: string | undefined, required: boolean }[];
    hidden: boolean;
    onSubmit: (values: NamedValue[]) => Promise<void>;
    onCancel?: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

export class AddForm extends React.Component<Props> {
    handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const inputs = Array.from(form.elements).filter(item => item.tagName === 'INPUT');
        let values = new Array<NamedValue>(0);
        inputs.forEach(element => {
            const input = element as HTMLInputElement;
            values.push({name: input.name, value: input.value});
            input.value = '';
        });
        await this.props.onSubmit(values);
        this.setState({});
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
        const inputs = this.props.inputs.map((input, index) => (
            <div key={index}>
                <input
                    className={globalStyles.shadowed}
                    type={input.type}
                    name={input.name}
                    placeholder={input.placeholder}
                    required={input.required}
                />
            </div>
        ));
        return (
            <form onSubmit={this.handleSubmit} hidden={this.props.hidden} className={styles['add-form']}>
                <div className={styles['lookup']}>
                    <div>
                        <h2>{this.props.header}</h2>
                    </div>
                    {inputs}
                    <div className={styles['buttons-wrapper']}>
                        <RoundButton radius={3} type="submit">✓</RoundButton>
                        <RoundButton radius={3} onClick={this.resetForm} color={Colors.Red}>×</RoundButton>
                    </div>
                </div>
            </form>
        );
    }

}