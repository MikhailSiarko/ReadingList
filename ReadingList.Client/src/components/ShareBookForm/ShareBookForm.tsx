import * as React from 'react';
import { Form } from '../Form';
import { NamedValue, SelectListItem, ListInfo } from '../../models';
import MultiSelect from '../MultiSelect';

interface Props {
    choosenBookId: number | null;
    options: ListInfo[];
    onSubmit: (bookId: number, ids: number[]) => Promise<void>;
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

class ShareBookForm extends React.Component<Props> {
    static mapListInfo(info: ListInfo): SelectListItem {
        return {
            text: `${info.name} (${info.ownerLogin}, ${info.type === 1 ? 'private' : 'shared'})`,
            value: info.id
        };
    }

    handleFormSubmit = async (values: NamedValue[]) => {
        const bookId = values.filter(v => v.name === 'book-id')[0].value;
        const ids = (values.filter(v => v.name === 'selected-lists')[0].value as SelectListItem[])
            .map(i => parseInt(i.value, 10));
        await this.props.onSubmit(parseInt(bookId, 10), ids);
    }

    handleCancel = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if(event.currentTarget.form) {
            Array.from(event.currentTarget.form.elements).forEach(i => {
                if(i.tagName === 'SELECT') {
                    const select = i as HTMLSelectElement;
                    Array.from(select.selectedOptions).forEach(o => {
                        o.selected = false;
                    });
                } else {
                    const input = i as HTMLSelectElement;
                    input.value = '';
                }
            });
        }
        this.props.onCancel(event);
    }

    render() {
        return (
            <Form
                header={'Share item'}
                size={
                    {
                        height: '19.5rem',
                        width: '40rem'
                    }
                }
                onSubmit={this.handleFormSubmit}
                onCancel={this.handleCancel}
            >
                <input
                    hidden={true}
                    name="book-id"
                    value={this.props.choosenBookId ? this.props.choosenBookId.toString() : ''}
                    readOnly={true}
                />
                <div>
                    <MultiSelect
                        name="selected-lists"
                        options={this.props.options.map(ShareBookForm.mapListInfo)}
                        required={true}
                        placeholder="Select lists"
                     />
                </div>
            </Form>
        );
    }
}

export default ShareBookForm;