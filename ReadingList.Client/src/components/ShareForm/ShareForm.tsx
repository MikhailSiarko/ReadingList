import * as React from 'react';
import { Form } from '../Form';
import { NamedValue } from '../../models';
import globalStyles from '../../styles/global.scss';

interface Props {
    onSubmit: (name: string) => Promise<void>;
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

class ShareForm extends React.Component<Props> {
    handleFormSubmit = async (values: NamedValue[]) => {
        await this.props.onSubmit(values[0].value);
    }

    handleCancel = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if(event.currentTarget.form) {
            Array.from(event.currentTarget.form.elements).forEach(i => {
                if(i.tagName === 'INPUT') {
                    const input = i as HTMLInputElement;
                    input.value = '';
                }
            });
        }
        this.props.onCancel(event);
    }

    render() {
        return (
            <Form
                header={'Share list'}
                size={
                    {
                        height: '19.5rem',
                        width: '40rem'
                    }
                }
                onSubmit={this.handleFormSubmit}
                onCancel={this.handleCancel}
            >
                <div>
                    <input
                        className={globalStyles.shadowed}
                        type="text"
                        name="shared-list-name"
                        placeholder="Enter shared list name"
                    />
                </div>
            </Form>
        );
    }
}

export default ShareForm;