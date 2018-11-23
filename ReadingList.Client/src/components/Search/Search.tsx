import * as React from 'react';
import styles from './Search.css';
import globalStyles from '../../styles/global.css';
import { isNullOrEmpty } from '../../utils';

interface Props {
    onSubmit: (search: string) => Promise<any[]>;
    itemRender: (item: any) => JSX.Element;
    onItemClick: (item: any) => Promise<void>;
}

interface State {
    query: string;
    timer: NodeJS.Timer | null;
    searchItems: any[];
}

class Search extends React.Component<Props, State> {
    private wrapper: HTMLDivElement;
    private input: HTMLInputElement;

    constructor(props: Props) {
        super(props);
        this.state = {timer: null, query: '', searchItems: new Array<any>(0)};
    }

    handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        clearTimeout(this.state.timer as NodeJS.Timer);
        this.setState({
            query: event.target.value, timer: setTimeout(async () => {
                await this.findItems();
            }, 500)
        });
    }

    handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        await this.findItems();
    }

    handleItemClick = async (event: React.MouseEvent<HTMLElement>) => {
        event.preventDefault();
        const dataIndex = this.findTarget(event).dataset.itemIndex;
        const index = parseInt(dataIndex as string, 10);
        await this.props.onItemClick(this.state.searchItems[index]);
        clearTimeout(this.state.timer as NodeJS.Timer);
    }

    componentWillUnmount() {
        if (this.state.timer) {
            clearTimeout(this.state.timer);
        }
        document.removeEventListener('click', this.handleWindowClick);
    }

    componentDidMount() {
        document.addEventListener('click', this.handleWindowClick);
    }

    handleWindowClick = (event: MouseEvent) => {
        const target = event.target as Element;
        if (this.wrapper) {
            const searchList = this.wrapper.lastElementChild;
            if (searchList && searchList.tagName === 'UL' && !Object.is(target, this.input)) {
                if (!Object.is(target, searchList)) {
                    if (this.state.timer) {
                        clearTimeout(this.state.timer);
                    }
                    this.setState({searchItems: new Array<any>(0), timer: null});
                }
            }
        }
    }

    handleFocus = async () => {
        if (this.state.searchItems.length === 0 && !isNullOrEmpty(this.state.query)) {
            await this.findItems();
        }
    }

    findItems = async () => {
        const items = await this.props.onSubmit(this.state.query as string);
        if (items) {
            this.setState({searchItems: Array.from(items)});
        }
    }

    findTarget = (event: React.MouseEvent<HTMLElement>) => {
        let target = event.target as HTMLElement;
        let continueIteration = true;
        while (continueIteration) {
            if (target.tagName === 'LI') {
                continueIteration = false;
            } else {
                target = target.parentElement as HTMLElement;
            }
        }
        return target;
    }

    mapItem = (item: any, index: number) => {
        return (
            <li key={index} data-item-index={index} onClick={this.handleItemClick}>
                {
                    this.props.itemRender(item)
                }
            </li>
        );
    }

    render() {
        return (
            <div className={styles['search-wrapper']} ref={ref => this.wrapper = (ref as HTMLDivElement)}>
                <form onSubmit={this.handleSubmit} autoComplete="off">
                    <input
                        autoFocus={true}
                        ref={ref => this.input = (ref as HTMLInputElement)}
                        onFocus={this.handleFocus}
                        className={globalStyles.shadowed}
                        type="search"
                        placeholder="Search..."
                        name="searchInput"
                        onChange={this.handleChange}
                        value={this.state.query}
                    />
                </form>
                {
                    this.state.searchItems.length > 0
                        ? (
                            <ul className={globalStyles.shadowed}>
                                {
                                    this.state.searchItems.map(this.mapItem)
                                }
                            </ul>
                        )
                        : null
                }
            </div>
        );
    }
}

export default Search;