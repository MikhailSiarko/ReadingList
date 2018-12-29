import * as React from 'react';
import Colors from 'src/styles/colors';
import styles from './Form.css';
import RoundButton from '../RoundButton';
import { NamedValue } from '../../models';
import { isNullOrEmpty } from '../../utils';

interface Props {
    header: string | JSX.Element;
    hidden: boolean;
    size?: {
        width: string;
        height: string;
    };
    onSubmit: (values: NamedValue[]) => Promise<void>;
    onCancel?: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

export class Form extends React.Component<Props> {
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

    render() {
        return (
            <form
                onSubmit={this.handleSubmit}
                hidden={this.props.hidden}
                className={styles['form']}
            >
                <div
                    className={styles['lookup']}
                    style={this.props.size ? this.props.size : undefined}
                >
                    <div className={styles.header}>
                        <h2>{this.props.header}</h2>
                        <hr />
                    </div>
                    <div className={styles['lookup-content']}>
                        {this.props.children}
                    </div>
                    <div className={styles['buttons-wrapper']}>
                        <hr />
                        <RoundButton radius={3} type="submit" title="Submit">
                            <i className="fas fa-check" />
                        </RoundButton>
                        <RoundButton
                            radius={3}
                            onClick={this.props.onCancel}
                            color={Colors.Red}
                            title="Cancel"
                        >
                            <i className="fas fa-times" />
                        </RoundButton>
                    </div>
                </div>
            </form>
        );
    }

}