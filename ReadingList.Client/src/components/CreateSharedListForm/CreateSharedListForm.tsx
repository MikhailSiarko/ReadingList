import * as React from 'react';
import Container from '../Container';
import MultiSelect from '../MultiSelect';
import { SelectListItem, NamedValue } from '../../models';
import { Form } from '../Form';

interface Props {
    tags: SelectListItem[];
    onSubmit: (name: string, tags: NamedValue[]) => void;
    onCancel: () => void;
}

interface State {
    chosenTags: SelectListItem[] | undefined;
    name: string | undefined;
}

class CreateSharedListForm extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = { chosenTags: undefined, name: undefined };
    }

    handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if(this.state.name && this.state.chosenTags) {
            const values = this.state.chosenTags.map(i => {
                return { name: i.text, value: i.value };
            });
            this.props.onSubmit(this.state.name, values);
        }
    }

    handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        this.setState({ name: event.target.value });
    }

    handleTagsChange = (tags: SelectListItem[]) => {
        this.setState({ chosenTags: tags });
    }

    render() {
        return (
            <Form
                header={'Add new list'}
                onSubmit={this.handleSubmit}
                onCancel={this.props.onCancel}
            >
                <Container width={80} unit={'%'}>
                    <div>
                        <input
                            type="text"
                            name="name"
                            required={true}
                            placeholder="Enter the name"
                            value={this.state.name}
                            onChange={this.handleNameChange}
                        />
                    </div>
                    <div>
                        <MultiSelect
                            name="tags"
                            placeholder={'Select tags'}
                            options={this.props.tags}
                            selectedFormat={item => `#${item.text}`}
                            value={this.state.chosenTags}
                            addNewIfNotFound={true}
                            onChange={this.handleTagsChange}
                        />
                    </div>
                </Container>
            </Form>

        );
    }
}

export default CreateSharedListForm;