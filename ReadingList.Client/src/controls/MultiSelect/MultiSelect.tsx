import * as React from 'react';
import { SelectListItem } from '../../models';
import { Selected } from './Selected';
import selectStyles from './MultiSelect.scss';
import { isNullOrEmpty } from '../../utils';

interface Props {
    options: SelectListItem[];
    value?: SelectListItem[];
    name: string;
    onChange?: (values: SelectListItem[]) => void;
    placeholder?: string;
    required?: boolean;
    addNewIfNotFound?: boolean;
    optionFormat?: (item: SelectListItem) => string;
    selectedFormat?: (item: SelectListItem) => string;
}

interface State {
    options: SelectListItem[];
    value: SelectListItem[];
    optionsHidden: boolean;
    search: string;
    addHidden: boolean;
    selectOptions: JSX.Element[];
}

export class MultiSelect extends React.Component<Props, State> {
    private wrapper: HTMLDivElement;

    static renderOptions = (props: Props, ...optionsToAdd: SelectListItem[]) => {
        if(props.options) {
            let options = props.options;
            if(optionsToAdd) {
                options = options.concat(optionsToAdd);
            }
            return options.map((option, index) => (
                <option
                    value={JSON.stringify(option)}
                    key={index}
                >
                    {
                        option.text
                    }
                </option>
            ));
        }
        return new Array<JSX.Element>(0);
    }

    constructor(props: Props) {
        super(props);
        this.state = this.createStateWithDefault();
    }

    getWrapperRef = (ref: HTMLDivElement) => {
        this.wrapper = ref;
    }

    createStateWithDefault = () => {
        let chosenOptions = new Array<SelectListItem>(0);
        let options = new Array<SelectListItem>(0);

        if(this.props.options) {
            options = this.props.options;
        }

        const value = this.props.value as SelectListItem[];
        if(value) {
            chosenOptions = options.filter(
                o => value.some(this.buildTextPredicate(o, true))
            );
            options = options.filter(i => value.every(this.buildTextPredicate(i)));
        }

        return {
            value: chosenOptions,
            options,
            optionsHidden: true,
            search: '',
            addHidden: true,
            selectOptions: MultiSelect.renderOptions(this.props)
        };
    }

    stopPropagation = (event: React.SyntheticEvent<any>) => {
        event.preventDefault();
        event.stopPropagation();
        event.nativeEvent.stopImmediatePropagation();
    }

    handleOptionClick = (event: React.MouseEvent<HTMLLIElement>) => {
        this.stopPropagation(event);
        const value = (event.currentTarget as HTMLLIElement).dataset.itemValue;
        const optionIndex = this.state.options.findIndex(item => item.value.toString() === value);
        const option = this.state.options[optionIndex];
        if(!this.state.value.includes(option)) {
            const newChosen = [...this.state.value];
            newChosen.push(option);
            const newOptions = [...this.state.options];
            newOptions.splice(optionIndex, 1);
            this.setState({
                value: newChosen,
                options: newOptions
            });

            if(this.props.onChange) {
                this.props.onChange(newChosen);
            }
        }
    }

    handleOptionRemove = (option: SelectListItem) => {
        function buildPredicate(item1: SelectListItem) {
            return function(item2: SelectListItem) {
                return item1.value === item2.value && item1.text === item2.text;
            };
        }

        const predicate = buildPredicate(option);

        let newChosen: SelectListItem[];
        let removedIndex: number;

        if(!option.value) {
            newChosen = [...this.state.value];
            removedIndex = this.state.value.findIndex(predicate);
            newChosen.splice(removedIndex, 1);
            this.setState({
                value: newChosen,
                options: [...this.state.options]
            });
            if(this.props.onChange) {
                this.props.onChange(newChosen);
            }
        } else {
            if(this.props.options) {
                const optionsIndex = this.props.options.findIndex(predicate);
                removedIndex = this.state.value.findIndex(predicate);
                newChosen = [...this.state.value];
                let removed = newChosen.splice(removedIndex, 1);
                const newOptions = [...this.state.options];
                newOptions.splice(optionsIndex, 0, ...removed);
                this.setState({
                    value: newChosen,
                    options: newOptions
                });
                if(this.props.onChange) {
                    this.props.onChange(newChosen);
                }
            }
        }
    }

    handleClick = (event: React.MouseEvent<HTMLDivElement>) => {
        event.preventDefault();
        this.setState({
            optionsHidden: !this.state.optionsHidden
        });
    }

    buildTextPredicate(item1: SelectListItem, equal: boolean = false) {
        if(equal) {
            return function(item2: SelectListItem) {
                return item2.text === item1.text;
            };
        }
        return function(item2: SelectListItem) {
            return item2.text !== item1.text;
        };
    }

    handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        if(this.props.options) {
            if(isNullOrEmpty(event.currentTarget.value)) {
                const newOptions = this.props.options.filter(
                    i => this.state.value.every(this.buildTextPredicate(i))
                );
                this.setState({
                    search: '',
                    options: newOptions
                });
            } else {
                const filtered = this.props.options.filter(
                    o => o.text.toLowerCase().includes(event.currentTarget.value) &&
                        this.state.value.every(this.buildTextPredicate(o))
                );
                if(filtered.length === 0 && this.props.addNewIfNotFound) {
                    this.setState({
                        search: event.currentTarget.value,
                        addHidden: false,
                        options: filtered
                    });
                } else {
                    this.setState({
                        search: event.currentTarget.value,
                        addHidden: true,
                        options: filtered
                    });
                }
            }
        }
    }

    handleWindowClick = (event: MouseEvent) => {
        const target = event.target as Node;
        if (this.wrapper && target !== this.wrapper) {
            const list = this.wrapper.lastElementChild;
            if (list) {
                const wasOutside = target.contains(list);
                if (wasOutside) {
                    this.setState({optionsHidden: true});
                }
            }
        }
    }

    componentDidMount() {
        document.addEventListener('click', this.handleWindowClick);
    }

    componentWillUnmount() {
        document.removeEventListener('click', this.handleWindowClick);
    }

    handleAddOption = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if(this.props.options) {
            const text = this.state.search;
            const newOption = {
                text,
                value: 0
            };
            this.setState({
                value: [
                    ...this.state.value,
                    newOption
                ],
                search: '',
                addHidden: true,
                options: this.props.options.filter(i => this.state.value.every(this.buildTextPredicate(i))),
                selectOptions: MultiSelect.renderOptions(this.props, newOption)
            });
        }
    }

    render() {
        return (
            <div className={selectStyles.wrapper}>
                <div
                    ref={this.getWrapperRef}
                    tabIndex={0}
                    onClick={this.handleClick}
                    className={selectStyles.select}
                >
                    <select
                        name={this.props.name}
                        hidden={true}
                        disabled={true}
                        required={this.props.required}
                        multiple={true}
                        value={this.state.value.map(option => JSON.stringify(option))}
                    >
                        {
                            this.state.selectOptions
                        }
                    </select>
                    {
                        this.state.value.length > 0
                            ? (
                                <div>
                                    {
                                        this.state.value.map((option, index) => (
                                            <Selected
                                                item={option}
                                                key={index}
                                                onRemove={this.handleOptionRemove}
                                                format={this.props.selectedFormat}
                                            />
                                        ))
                                    }
                                </div>
                            )
                            : this.props.placeholder
                    }
                    <div onClick={this.stopPropagation}>
                        <div
                            className={selectStyles.options}
                            hidden={this.state.optionsHidden}
                        >
                            <div>
                                <input
                                    type="text"
                                    value={this.state.search}
                                    onChange={this.handleSearchChange}
                                    placeholder={'Type to search'}
                                />
                                <button
                                    type="button"
                                    title="Add as chosen option"
                                    hidden={this.state.addHidden}
                                    onClick={this.handleAddOption}
                                >✓</button>
                            </div>
                            <ul>
                                {
                                    this.state.options.map((item, index) => (
                                        <li
                                            className={selectStyles['select-item']}
                                            data-item-value={item.value}
                                            onClick={this.handleOptionClick}
                                            key={index}
                                        >
                                            {this.props.optionFormat ? this.props.optionFormat(item) : item.text}
                                        </li>
                                    ))
                                }
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}