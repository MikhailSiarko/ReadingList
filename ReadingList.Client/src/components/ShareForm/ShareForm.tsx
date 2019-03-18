import * as React from 'react';
import { Form } from '../Form';

interface Props {
    onSubmit: (name: string) => void;
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

interface State {
    name?: string;
}

class ShareForm extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = { name: undefined };
    }

    handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if(this.state.name) {
            this.props.onSubmit(this.state.name);
        }
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

    handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        this.setState({ name: event.target.value });
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
                onSubmit={this.handleSubmit}
                onCancel={this.handleCancel}
            >
                <div>
                    <input
                        type="text"
                        name="shared-list-name"
                        placeholder="Enter shared list name"
                        value={this.state.name}
                        onChange={this.handleNameChange}
                    />
                </div>
            </Form>
        );
    }
}

export default ShareForm;