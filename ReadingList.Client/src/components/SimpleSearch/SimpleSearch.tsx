import * as React from 'react';
import styles from './SimpleSearch.scss';

interface Props {
    query?: string;
    onChange: (query: string) => void;
}

interface State {
    timer: NodeJS.Timer | null;
}

class SimpleSearch extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {timer: null};
    }

    handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        clearTimeout(this.state.timer as NodeJS.Timer);
        const value = event.target.value;
        this.setState({
            timer: setTimeout(async () => {
                await this.props.onChange(value);
            }, 500)
        });
    }

    componentWillUnmount() {
        if (this.state.timer) {
            clearTimeout(this.state.timer);
        }
    }

    render() {
        return (
            <div className={styles['search-wrapper']}>
                <input
                    autoFocus={true}
                    type="search"
                    placeholder="Search..."
                    name="searchInput"
                    defaultValue={this.props.query}
                    onChange={this.handleChange}
                />
            </div>
        );
    }
}

export default SimpleSearch;