import * as React from 'react';

interface Props {
    query?: string;
}

const Search: React.SFC<Props> = props => (
    <div>
        <input type="search" name="searchInput" value={props.query} />
    </div>
);

export default Search;