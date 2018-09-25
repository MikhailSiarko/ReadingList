import * as React from 'react';
import Search from '../../components/Search';
import Grid from '../../components/Grid';

interface Props {
    sharedLists: any;
}

class SharedBookLists extends React.Component<Props> {
    render() {
        return (
            <div>
                <Search />
                <Grid items={[]} />
            </div>
        );
    }
}

export default SharedBookLists;