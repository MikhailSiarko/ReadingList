import * as React from 'react';
import { SharedBookList as SharedList } from '../../models/BookList/Implementations/SharedBookList';
import { SharedBookListService } from '../../services/SharedBookListService';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import BookList from '../../components/BookList';
import SharedBookLI from '../../components/SharedBookLI/SharedBookLI';

interface Props extends RouteComponentProps<any> {
    getList: (id: number) => Promise<SharedList>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

interface State {
    list: SharedList | null;
}

class SharedBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {list: null};
    }

    async componentDidMount() {
        if(this.state.list == null) {
            const id = parseInt(this.props.match.params.id, 10);
            const list = await this.props.getList(id);
            this.setState({list});
        }
    }

    render() {
        let listItems;
        if(this.state.list && this.state.list.items.length > 0) {
            listItems = this.state.list.items.map(
                listItem => <SharedBookLI key={listItem.id} item={listItem} />);
        }
        if(this.state.list) {
            const legend = (
                <div>
                    <h4 style={{margin: 0}}>{this.state.list.name}</h4>
                    <p style={{margin: 0}}>
                        {
                            this.state.list.tags.reduce((acc, tag) => acc + ' #' + tag, '').substring(1)
                        }
                    </p>
                </div>
            );
            return <BookList items={listItems} legend={legend}/>;
        }
        return null;
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const bookService = new SharedBookListService();
    return {
        getList: async (id: number) => {
            const result = await bookService.getList(id); 
            return result.data as SharedList;
        },
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        }
    };
}

export default connect(null, mapDispatchToProps)(SharedBookList);