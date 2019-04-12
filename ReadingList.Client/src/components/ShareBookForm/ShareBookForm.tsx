import * as React from 'react';
import { Form, MultiSelect } from '../../controls';
import { SelectListItem, ListInfo } from '../../models';

interface Props {
    chosenBookId: number | null;
    options: ListInfo[];
    onSubmit: (bookId: number, ids: number[]) => void;
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

interface State {
    selectedLists?: SelectListItem[];
}

class ShareBookForm extends React.Component<Props, State> {
    static mapListInfo(info: ListInfo): SelectListItem {
        return {
            text: `${info.name} (${info.ownerLogin}, ${info.type === 1 ? 'private' : 'shared'})`,
            value: info.id
        };
    }

    constructor(props: Props) {
        super(props);
        this.state = { selectedLists: undefined };
    }

    handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if(this.props.chosenBookId && this.state.selectedLists) {
            this.props.onSubmit(this.props.chosenBookId, this.state.selectedLists.map(list => {
                return parseInt(list.value, 10);
            }));
        }
    }

    handleCancel = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel(event);
    }

    handleListsChange = (lists: SelectListItem[]) => {
        this.setState({ selectedLists: lists });
    }

    render() {
        return (
            <Form
                header={'Share item'}
                size={
                    {
                        height: '27.5rem',
                        width: '40rem'
                    }
                }
                onSubmit={this.handleSubmit}
                onCancel={this.handleCancel}
            >
                <input
                    hidden={true}
                    name="book-id"
                    value={this.props.chosenBookId ? this.props.chosenBookId.toString() : ''}
                    readOnly={true}
                />
                <div>
                    {
                        this.props.options &&
                        <MultiSelect
                            name="selected-lists"
                            options={this.props.options.map(ShareBookForm.mapListInfo)}
                            required={true}
                            placeholder="Select lists"
                            value={this.state.selectedLists}
                            onChange={this.handleListsChange}
                        />
                    }
                </div>
            </Form>
        );
    }
}

export default ShareBookForm;