import * as React from 'react';
import Search from '../../components/Search';
import Grid from '../../components/Grid';
import { SharedBookList } from '../../models/BookList/Implementations/SharedBookList';
import { SharedBookListService } from '../../services/SharedBookListService';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { createPropActionWithResult } from '../../utils';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';

interface Props extends RouteComponentProps<any> {
    getSharedLists: (query: string) => Promise<SharedBookList[]>;
    getOwnSharedLists: () => Promise<SharedBookList[]>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

interface State {
    sharedLists: SharedBookList[] | null;
}

class SharedBookLists extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {sharedLists: null};
    }

    searchHandler = async (query: string) => {
        let lists = await this.props.getSharedLists(query);
        this.setState({sharedLists: lists});
        this.props.history.replace(`/shared/search/${query}`, {from: this.props.location});
    }

    async componentDidMount() {
        if(this.state.sharedLists == null) {
            let lists;
            if(this.props.match.params.query === 'own') {
                lists = await this.props.getOwnSharedLists();
            } else {
                lists = await this.props.getSharedLists(
                    this.props.match.params.query
                        ? this.props.match.params.query
                        : '');
            }
            this.setState({sharedLists: lists});
        }
    }

    render() {
        let items;
        if(this.state.sharedLists) {
            items = this.state.sharedLists.map(
                list => {
                    return {
                        header: list.name,
                        content: list.tags.reduce((acc, tag) => acc + ' #' + tag, '').substring(1),
                        onClick: () => this.props.history.replace('/shared/' + list.id,
                            {from: this.props.location})
                    };
                });
        }
        return (
            <div>
                <Search onSubmit={this.searchHandler} />
                <Grid items={items} />
            </div>
        );
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const bookService = new SharedBookListService();
    return {
        getSharedLists: createPropActionWithResult(bookService.getLists, dispatch),
        getOwnSharedLists: createPropActionWithResult(bookService.getOwnLists, dispatch),
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        }
    };
}

export default connect(null, mapDispatchToProps)(SharedBookLists);