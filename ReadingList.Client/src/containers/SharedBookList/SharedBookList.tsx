import * as React from 'react';
import { SharedBookList as SharedList } from '../../models/BookList/Implementations/SharedBookList';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import BookList from '../../components/BookList';
import SharedBookLI from '../../components/SharedBookLI';
import { withSpinner } from '../../hoc';
import { SharedBookListItem } from '../../models/BookList';
import Search from '../../components/Search';
import { Book } from '../../models';
import { RootState } from '../../store/reducers';
import { connect, Dispatch } from 'react-redux';
import { SharedBookListService } from '../../services';
import { BookService } from '../../services/BookService';
import { cloneDeep } from 'lodash';
import { reduceTags, processFailedRequest } from '../../utils';
import BookSearchItem from '../../components/BookSearchItem';

interface Props extends RouteComponentProps<any> {
    loading: boolean;
    getList: (id: number) => Promise<SharedList>;
    addItem: (listId: number, bookId: number) => Promise<SharedBookListItem>;
    loadingStart: () => void;
    loadingEnd: () => void;
    findBooks: (query: string) => Promise<Book[]>;
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
        if (this.state.list == null) {
            const id = parseInt(this.props.match.params.id, 10);
            this.props.loadingStart();
            const list = await this.props.getList(id);
            this.props.loadingEnd();
            this.setState({list});
        }
    }

    handleSearchItemClick = async (item: Book) => {
        this.props.loadingStart();
        let copy;
        if (this.state.list) {
            const bookItem = await this.props.addItem(this.state.list.id, item.id);
            if(bookItem) {
                copy = cloneDeep(this.state.list);
                copy.items.push(bookItem);
            }
        }
        this.props.loadingEnd();
        if (copy) {
            this.setState({list: copy});
        }
    }

    renderLegend = () => {
        if (this.state.list) {
            return (
                <div>
                    <h4 style={{margin: 0}}>{this.state.list.name}</h4>
                    <p style={{margin: 0}}>
                        {
                            reduceTags(this.state.list.tags)
                        }
                    </p>
                </div>
            );
        } else {
            return '';
        }
    }

    renderSearchItem = (item: Book) => <BookSearchItem book={item} />;

    mapItem = (item: SharedBookListItem) => <SharedBookLI key={item.id} item={item} />;

    render() {
        const Spinnered = withSpinner(this.state.list && !this.props.loading, () => {
            if (this.state.list) {
                return (
                    <>
                        {
                            (this.state.list && this.state.list.editable) && (
                                <Search
                                    onSubmit={this.props.findBooks}
                                    itemRender={this.renderSearchItem}
                                    onItemClick={this.handleSearchItemClick}
                                />
                            )
                        }
                        <BookList items={this.state.list.items.map(this.mapItem)} legend={this.renderLegend()} />
                    </>
                );
            }
            return null;
        });
        return <Spinnered />;
    }
}

function mapStateToProps(state: RootState) {
    return {
        loading: state.loading
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const listService = new SharedBookListService();
    const bookService = new BookService();
    return {
        getList: async (id: number) => {
            const result = await listService.getList(id);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        addItem: async (listId: number, bookId: number) => {
            const result = await listService.addItem(listId, bookId);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        },
        findBooks: async (query: string) => {
            const result = await bookService.findBooks(query);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SharedBookList);