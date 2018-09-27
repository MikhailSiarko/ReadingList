import * as React from 'react';
import styles from './Search.css';

interface Props {
    query?: string;
    onSubmit: (query: string) => void;
}

function onSubmitHandler(event: React.FormEvent<HTMLFormElement>, onSubmit: (query: string) => void) {
    event.preventDefault();
    const form = event.target as HTMLFormElement;
    let query = (form.elements[0] as HTMLInputElement).value;
    onSubmit(query);
}

const Search: React.SFC<Props> = props => (
    <div className={styles['search-wrapper']}>
        <form
            onSubmit={(event: React.FormEvent<HTMLFormElement>) => onSubmitHandler(event, props.onSubmit)}>
            <input type="search" placeholder="Search..." name="searchInput" value={props.query} />
        </form>
    </div>
);

export default Search;