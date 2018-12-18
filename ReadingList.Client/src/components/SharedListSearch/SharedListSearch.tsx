import * as React from 'react';
import styles from './SharedListSearch.css';
import globalStyles from '../../styles/global.css';

interface Props {
    query?: string;
    onSubmit: (query: string) => void;
}

interface State {
    query?: string;
    timer: NodeJS.Timer | null;
}

class SharedListSearch extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {timer: null, query: props.query ? props.query : ''};
    }

    handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        clearTimeout(this.state.timer as NodeJS.Timer);
        this.setState({
            query: event.target.value,
            timer: setTimeout(async () => {
                await this.findItems();
            }, 500)
        });
    }

    componentWillUnmount() {
        if (this.state.timer) {
            clearTimeout(this.state.timer);
        }
    }

    handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        await this.findItems();
    }

    findItems = () => {
        this.props.onSubmit(this.state.query ? this.state.query : '');
    }

    render() {
        return (
            <div className={styles['search-wrapper']}>
                <form onSubmit={this.handleSubmit}>
                    <input
                        autoFocus={true}
                        className={globalStyles.shadowed}
                        type="search"
                        placeholder="Search..."
                        name="searchInput"
                        value={this.state.query}
                        onChange={this.handleChange}
                    />
                </form>
            </div>
        );
    }
}

export default SharedListSearch;