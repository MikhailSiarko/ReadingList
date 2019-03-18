import * as React from 'react';
import Colors from '../../styles/colors';
import styles from './SharedListEditForm.scss';
import RoundButton from '../RoundButton';
import MultipleSelect from '../MultiSelect';
import { Moderator, Tag, SelectListItem } from '../../models';

interface Props {
    name: string;
    tags: Tag[];
    tagsOptions: SelectListItem[];
    moderators: Moderator[];
    moderatorsOptions: SelectListItem[];
    onSave: (newName: string, tags: Tag[], moderators: number[]) => void;
    onCancel: () => void;
}

interface State {
    tags: SelectListItem[];
    moderators: SelectListItem[];
    name: string;
}

class SharedListEditForm extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            moderators: props.moderators.map(this.mapModerator),
            name: props.name,
            tags: props.tags.map(this.mapTag)
        };
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        this.props.onSave(
            this.state.name,
            this.state.tags.map(t => {
                return { id: parseInt(t.value, 10), name: t.text };
            }),
            this.state.moderators.map(m => parseInt(m.value, 10))
        );
    }

    cancelHandler = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
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

    handleTagsChange = (tags: SelectListItem[]) => {
        this.setState({ tags });
    }

    handleModeratorsChange = (moderators: SelectListItem[]) => {
        this.setState({ moderators });
    }

    handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        this.setState({ name: event.target.value });
    }

    render() {
        return (
            <form onSubmit={this.submitHandler} className={styles['edit-form']}>
                <div className={styles['name-editor']}>
                    <input
                        name={'name'}
                        type={'text'}
                        required={true}
                        value={this.state.name}
                        onChange={this.handleNameChange}
                    />
                </div>
                <div className={styles['tags-select']}>
                    {
                        this.props.tagsOptions &&
                            (
                                <MultipleSelect
                                    name={'tags'}
                                    options={this.props.tagsOptions}
                                    value={this.props.tags.map(this.mapTag)}
                                    selectedFormat={item => `#${item.text}`}
                                    addNewIfNotFound={true}
                                    placeholder={'Select tags'}
                                    onChange={this.handleTagsChange}
                                />
                            )
                    }
                </div>
                <div className={styles['moderators']}>
                    {
                        this.props.moderatorsOptions &&
                            (
                                <MultipleSelect
                                    name={'moderators'}
                                    options={this.props.moderatorsOptions}
                                    value={this.props.moderators.map(this.mapModerator)}
                                    selectedFormat={item => item.text}
                                    addNewIfNotFound={false}
                                    placeholder={'Select moderators'}
                                    onChange={this.handleModeratorsChange}
                                />
                            )
                    }
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