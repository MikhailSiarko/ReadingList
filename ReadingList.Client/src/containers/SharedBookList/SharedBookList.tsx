import * as React from 'react';
import { SharedBookList as SharedList } from '../../models/BookList/Implementations/SharedBookList';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import BookList from '../../components/BookList';
import SharedBookLI from '../../components/SharedBookLI';
import { SharedBookListItem, SelectListItem } from '../../models/BookList';
import { Book } from '../../models';
import { RootState } from '../../store/reducers';
import { connect, Dispatch } from 'react-redux';
import { SharedBookListService } from '../../services';
import { BookService } from '../../services/BookService';
import { cloneDeep } from 'lodash';
import { reduceTags, processFailedRequest } from '../../utils';
import { withContextMenu } from '../../hoc';
import SharedListEditForm from '../../components/SharedListEditForm';
import { TagsService } from '../../services/TagsService';
import { Tag } from '../../models/Tag';
import FixedButton from '../../components/FixedButton';
import AddBookForm from '../../components/AddBookForm/AddBookForm';

interface Props extends RouteComponentProps<any> {
    getList: (id: number) => Promise<SharedList>;
    getTags: () => Promise<Tag[]>;
    addItem: (listId: number, bookId: number) => Promise<SharedBookListItem>;
    updateList: (id: number, name: string, tags: Tag[]) => Promise<SharedList>;
    loadingStart: () => void;
    loadingEnd: () => void;
    findBooks: (query: string) => Promise<Book[]>;
}

interface State {
    list: SharedList | null;
    isInEditMode: boolean;
    options: SelectListItem[] | null;
    isFormHidden: boolean;
    books: Book[] | null;
    bookSearchQuery: string | null;
}

class SharedBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            list: null,
            isInEditMode: false,
            options: null,
            bookSearchQuery: null,
            books: null,
            isFormHidden: true
        };
    }

    async componentDidMount() {
        if (this.state.list === null) {
            const id = parseInt(this.props.match.params.id, 10);
            this.props.loadingStart();
            const list = await this.props.getList(id);
            const tags = await this.props.getTags();
            this.setState(
                {
                    list,
                    options: tags.map(t => {
                        return {
                            text: t.name,
                            value: t.id
                        };
                    })
                },
                () => this.props.loadingEnd()
            );
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

        if (copy) {
            this.setState({list: copy}, () => this.props.loadingEnd());
        }
    }

    handleSaveList = async (name: string, assignedTags: Tag[]) => {
        const list = this.state.list as SharedList;
        this.props.loadingStart();
        const updatedList = await this.props.updateList(list.id, name, assignedTags);
        if(updatedList) {
            let options = this.state.options as SelectListItem[];
            if(assignedTags.some(t => t.id.toString() === '0')) {
                const tags = await this.props.getTags();
                options = tags.map(o => {
                    return {
                        value: o.id,
                        text: o.name
                    };
                });
            }
            this.setState({list: updatedList, isInEditMode: false, options: options}, () => this.props.loadingEnd());
        } else {
            this.props.loadingEnd();
        }
    }

    switchToEditMode = () => {
        this.setState({isInEditMode: !this.state.isInEditMode});
    }

    renderLegend = () => {
        if (this.state.list) {
            if(this.state.isInEditMode) {
                return (
                    <SharedListEditForm
                        tags={this.state.list.tags}
                        name={this.state.list.name}
                        options={this.state.options as SelectListItem[]}
                        onSave={this.handleSaveList}
                        onCancel={this.switchToEditMode}
                    />
                );
            }
            return (
                <div>
                    <h4 style={{margin: 0, fontWeight: 400}}>{this.state.list.name.toUpperCase()}</h4>
                    <p style={{margin: 0}}>
                        {
                            reduceTags(this.state.list.tags.map(i => i.name))
                        }
                    </p>
                </div>
            );
        } else {
            return '';
        }
    }

    mapItem = (item: SharedBookListItem) => {
        const Contexed = withContextMenu([], SharedBookLI);
        return <Contexed key={item.id} item={item} />;
    }

    closeForm = (event: React.MouseEvent<HTMLButtonElement>) => {
        this.setState({
            isFormHidden: true,
            books: null,
            bookSearchQuery: null
        });
    }

    showBooksForm = async () => {
        this.props.loadingStart();
        const books = await this.props.findBooks('');
        if(books) {
            this.setState({
                books,
                isFormHidden: false
            }, () => this.props.loadingEnd());
        } else {
            this.setState({
                isFormHidden: false
            }, () => this.props.loadingEnd());
        }
    }

    handleSearchChange = async (query: string) => {
        this.props.loadingStart();
        const books = await this.props.findBooks(query);
        if(books) {
            this.setState({
                books,
                bookSearchQuery: query
            }, () => this.props.loadingEnd());
        } else {
            this.props.loadingEnd();
        }
    }

    handleAddBook = async (id: number) => {
        if (this.state.list) {
            this.props.loadingStart();
            const bookItem = await this.props.addItem(this.state.list.id, id);
            if(bookItem) {
                let copy = cloneDeep(this.state.list);
                copy.items.push(bookItem);
                if (copy) {
                    this.setState(
                        {
                            list: copy,
                            books: null,
                            isFormHidden: true
                        },
                        () => this.props.loadingEnd()
                    );
                }
            } else {
                this.setState(
                    {
                        books: null,
                        isFormHidden: true
                    },
                    () => this.props.loadingEnd()
                );
            }
        }
    }

    render() {
        let actions = [];

        if(this.state.list && this.state.list.editable) {
            actions.push({
                text: 'Edit',
                onClick: () => this.switchToEditMode()
            });
        }

        let Contexed = withContextMenu(actions, BookList);

        if (this.state.list) {
            return (
                <>
                    <Contexed items={this.state.list.items.map(this.mapItem)} legend={this.renderLegend()} />
                    {
                        this.state.list.editable && (
                            <>
                                <FixedButton
                                    radius={3}
                                    title="Add book"
                                    onClick={this.showBooksForm}
                                >+</FixedButton>
                                <AddBookForm
                                    hidden={this.state.isFormHidden}
                                    books={this.state.books ? this.state.books : []}
                                    searchQuery={this.state.bookSearchQuery ? this.state.bookSearchQuery : ''}
                                    onSubmit={this.handleAddBook}
                                    onCancel={this.closeForm}
                                    onQueryChange={this.handleSearchChange}
                                />
                            </>
                        )
                    }
                </>
            );
        }
        return null;
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const listService = new SharedBookListService();
    return {
        getList: async (id: number) => {
            const result = await listService.getList(id);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        getTags: async () => {
            const result = await new TagsService().getTags();
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        updateList: async (id: number, name: string, tags: Tag[]) => {
            const result = await listService.updateList({
                id,
                name,
                tags
            });
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
            const result = await new BookService().findBooks(query);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        }
    };
}

export default connect(null, mapDispatchToProps)(SharedBookList);