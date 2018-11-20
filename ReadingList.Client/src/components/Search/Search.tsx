import * as React from 'react';
import styles from './Search.css';
import globalStyles from '../../styles/global.css';

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
    private form: HTMLFormElement;

    constructor(props: Props) {
        super(props);
        this.state = {timer: null, query: '', searchItems: new Array<any>(0)};
    }

    handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        clearTimeout(this.state.timer as NodeJS.Timer);
        this.setState({query: event.target.value, timer: setTimeout(async () => {
            await this.findBooks();
        }, 1000)});
    }

    handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        await this.findBooks();
    }

    handleItemClick = async (event: React.MouseEvent<HTMLElement>) => {
        event.preventDefault();
        let target = event.target as HTMLElement;
        let continueIteration = true;
        while(continueIteration) {
            if(target.tagName === 'LI') {
                continueIteration = false;
            } else {
                target = target.parentElement as HTMLElement;
            }
        }
        const dataIndex = target.dataset.itemIndex;
        const index = parseInt(dataIndex as string, 10);
        const item = this.state.searchItems[index];
        await this.props.onItemClick(item);
        clearTimeout(this.state.timer as NodeJS.Timer);
    }

    componentWillUnmount() {
        if(this.state.timer) {
            clearTimeout(this.state.timer);
        }
    }

    render() {
        return (
            <div className={styles['search-wrapper']}>
                <form onSubmit={this.handleSubmit} ref={r => this.form = (r as HTMLFormElement)} autoComplete="off">
                    <input
                        autoFocus={true}
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
                                    this.state.searchItems.map((item, index) => (
                                        <li key={index} data-item-index={index} onClick={this.handleItemClick}>
                                            {
                                                this.props.itemRender(item)
                                            }
                                        </li>
                                    ))
                                }
                            </ul>
                        )
                        : null
                }
            </div>
        );
    }

    private findBooks = async () => {
        const items = await this.props.onSubmit(this.state.query as string);
        this.setState({searchItems: Array.from(items)});
    }
}

export default Search;