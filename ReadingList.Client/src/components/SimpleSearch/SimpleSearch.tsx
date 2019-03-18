import * as React from 'react';
import styles from './SimpleSearch.scss';

interface Props {
    query?: string;
    onChange: (query: string) => void;
}

class SimpleSearch extends React.Component<Props> {
    private timer: NodeJS.Timer;

    handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        event.preventDefault();
        clearTimeout(this.timer);
        const value = event.target.value;
        this.timer = setTimeout(() => {
            this.props.onChange(value);
        }, 500);
    }

    componentWillUnmount() {
        if (this.timer) {
            clearTimeout(this.timer);
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