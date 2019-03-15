import * as React from 'react';
import Colors from '../../styles/colors';
import styles from './SharedListEditForm.scss';
import RoundButton from '../RoundButton';
import MultipleSelect from '../MultiSelect';
import { Moderator, Tag, SelectListItem } from '../../models';

interface Props {
    name: string;
    tags: Tag[];
    tagsOptions: SelectListItem[] | null;
    moderators: Moderator[];
    moderatorsOptions: SelectListItem[] | null;
    onSave: (newName: string, tags: Tag[], moderators: number[]) => void;
    onCancel: () => void;
}

class SharedListEditForm extends React.Component<Props> {
    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.target as HTMLFormElement;
        const name = target.elements['name'].value;
        const selectedTags = (target.elements['tags'] as HTMLSelectElement).selectedOptions;
        const selectedModerators = (target.elements['moderators'] as HTMLSelectElement).selectedOptions;
        this.props.onSave(
            name,
            Array.from(selectedTags).map(i => {
                const option = (JSON.parse(i.value) as SelectListItem);
                return {
                    id: option.value,
                    name: option.text
                };
            }),
            Array.from(selectedModerators).map(i => {
                const option = (JSON.parse(i.value) as SelectListItem);
                return option.value;
            })
        );
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if(event.currentTarget.form) {
            Array.from(event.currentTarget.form.elements).forEach(i => {
                if(i.tagName === 'SELECT') {
                    const select = i as HTMLSelectElement;
                    Array.from(select.selectedOptions).forEach(o => {
                        o.selected = false;
                    });
                }
            });
        }
        this.props.onCancel();
    }

    mapTag(tag: Tag): SelectListItem {
        return {
            text: tag.name,
            value: tag.id
        };
    }

    mapModerator (moderator: Moderator): SelectListItem {
        return {
            text: moderator.login,
            value: moderator.id
        };
    }

    render() {
        return (
            <form onSubmit={this.submitHandler} className={styles['edit-form']}>
                <div className={styles['name-editor']}>
                    <input
                        name={'name'}
                        type={'text'}
                        required={true}
                        defaultValue={this.props.name}
                    />
                </div>
                <div className={styles['tags-select']}>
                    <MultipleSelect
                        name={'tags'}
                        options={this.props.tagsOptions}
                        value={this.props.tags.map(this.mapTag)}
                        selectedFormat={item => `#${item.text}`}
                        addNewIfNotFound={true}
                        placeholder={'Select tags'}
                    />
                </div>
                <div className={styles['moderators']}>
                    <MultipleSelect
                        name={'moderators'}
                        options={this.props.moderatorsOptions}
                        value={this.props.moderators.map(this.mapModerator)}
                        selectedFormat={item => item.text}
                        addNewIfNotFound={false}
                        placeholder={'Select moderators'}
                    />
                </div>
                <div>
                    <RoundButton radius={2} type={'submit'}>✓</RoundButton>
                </div>
                <div>
                    <RoundButton radius={2} onClick={this.cancelHandler} buttonColor={Colors.Red}>×</RoundButton>
                </div>
            </form>
        );
    }
}

export default SharedListEditForm;