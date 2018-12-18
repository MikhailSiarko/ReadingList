import * as React from 'react';
import { SelectListItem } from '../../models';
import Selected from './Selected';
import selectStyles from './Select.css';
import { applyClasses } from '../../utils';
import globalStyles from '../../styles/global.css';

interface Props {
    options: SelectListItem[] | null;
    name: string;
    placeholder?: string;
    required?: boolean;
    optionFormat?: (item: SelectListItem) => string;
    selectedFormat?: (item: SelectListItem) => string;
}

interface State {
    options: SelectListItem[];
    chosenOptions: SelectListItem[];
    optionsHidden: boolean;
    search: string;
}

class Select extends React.PureComponent<Props, State> {
    select: HTMLDivElement;

    constructor(props: Props) {
        super(props);
        this.state = {
            chosenOptions: new Array<SelectListItem>(0),
            options: this.props.options ? this.props.options : new Array<SelectListItem>(0),
            optionsHidden: true,
            search: ''
        };
    }

    handleOptionClick = (event: React.MouseEvent<HTMLLIElement>) => {
        event.preventDefault();
        const value = (event.currentTarget as HTMLLIElement).dataset.itemValue;
        const optionIndex = this.state.options.findIndex(item => item.value.toString() === value);
        const option = this.state.options[optionIndex];
        if(!this.state.chosenOptions.includes(option)) {
            const newState = [...this.state.chosenOptions];
            newState.push(option);
            this.setState({
                chosenOptions: newState,
                optionsHidden: true
            });
        }
    }

    handleRemove = (value: any) => {
        const filtered = this.state.chosenOptions.filter(item => item.value !== value);
        this.setState({chosenOptions: filtered});
    }

    handleFocus = () => {
        this.setState({
            optionsHidden: false
        });
    }

    handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        if(this.props.options) {
            const filtered = this.props.options.filter(
                option => option.text.toLowerCase().includes(event.currentTarget.value)
            );
            this.setState({
                search: event.currentTarget.value,
                options: filtered
            });
        }
    }

    handleWindowClick = (event: MouseEvent) => {
        const target = event.target as Node;
        if (this.select && target !== this.select) {
            const list = this.select.lastElementChild;
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

    render() {
        return (
            <div className={selectStyles.wrapper}>
                <div
                    ref={ref => this.select = (ref as HTMLDivElement)}
                    tabIndex={0}
                    onFocus={this.handleFocus}
                    onClick={this.handleFocus}
                    className={applyClasses(globalStyles.shadowed, selectStyles.select)}
                >
                    <input
                        name={this.props.name}
                        hidden={true}
                        required={this.props.required}
                        defaultValue={JSON.stringify(this.state.chosenOptions)}
                    />
                    {
                        this.state.chosenOptions.length > 0
                            ? (
                                <div>
                                    {
                                        this.state.chosenOptions.map((option, index) => (
                                            <Selected
                                                item={option}
                                                key={index}
                                                onRemove={this.handleRemove}
                                                format={this.props.selectedFormat}
                                            />
                                        ))
                                    }
                                </div>
                            )
                            : this.props.placeholder
                    }
                    <div>
                        <div
                            className={applyClasses(selectStyles.options, globalStyles.shadowed)}
                            hidden={this.state.optionsHidden}
                        >
                            <input
                                type="text"
                                value={this.state.search}
                                onChange={this.handleChange}
                                placeholder={'Type to search'}
                            />
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

export default Select;