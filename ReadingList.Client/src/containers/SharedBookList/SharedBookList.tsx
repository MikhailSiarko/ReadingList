import * as React from 'react';
import { SharedBookList as SharedList } from '../../models/BookList/Implementations/SharedBookList';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import BookList from '../../components/BookList';
import SharedBookLI from '../../components/SharedBookLI';
import { SharedBookListItem, SelectListItem, ListInfo } from '../../models/BookList';
import { Book, User, Chunked } from '../../models';
import { RootState } from '../../store/reducers';
import { connect, Dispatch } from 'react-redux';
import { SharedBookListService, BookService, UsersService, ListsService } from '../../services';
import { cloneDeep } from 'lodash';
import { processFailedRequest } from '../../utils';
import { withContextMenu, closeContextMenues } from '../../hoc';
import SharedListEditForm from '../../components/SharedListEditForm';
import { TagsService } from '../../services/TagsService';
import { Tag } from '../../models/Tag';
import AddBookForm from '../../components/AddBookForm/AddBookForm';
import FixedGroup from '../../components/FixedGroup';
import RoundButton from '../../components/RoundButton';
import SharedListLegend from '../../components/SharedListLegend';
import SharedBookForm from '../../components/SharedBookForm';

interface Props extends RouteComponentProps<any> {
    getList: (id: number) => Promise<SharedList>;
    getTags: () => Promise<Tag[]>;
    getUsers: () => Promise<User[]>;
    addItem: (listId: number, bookId: number) => Promise<SharedBookListItem>;
    removeItem: (listId: number, itemId: number) => Promise<number>;
    updateList: (id: number, name: string, tags: Tag[], moderators: number[]) => Promise<SharedList>;
    startLoading: () => void;
    endLoading: () => void;
    findBooks: (query: string, chunk: number) => Promise<Chunked<Book>>;
    getModeratedLists: () => Promise<ListInfo[]>;
    shareBook: (itemId: number, lists: number[]) => Promise<void>;
    removeList: (listId: number) => Promise<never>;
}

interface State {
    list: SharedList | null;
    isInEditMode: boolean;
    tagOptions: SelectListItem[] | null;
    moderatorOptions: SelectListItem[] | null;
    isFormHidden: boolean;
    books: Book[] | null;
    bookSearchQuery: string | null;
    sharingBookId: number | null;
    moderatedLists: ListInfo[] | null;
    shareBookFormHidden: boolean;
    hasPrevious: boolean;
    hasNext: boolean;
    chunk: number;
}

class SharedBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            list: null,
            isInEditMode: false,
            tagOptions: null,
            bookSearchQuery: null,
            books: null,
            isFormHidden: true,
            moderatorOptions: null,
            moderatedLists: null,
            shareBookFormHidden: true,
            sharingBookId: null,
            chunk: 1,
            hasNext: false,
            hasPrevious: false
        };
    }

    async componentDidMount() {
        if (this.state.list === null) {
            const id = parseInt(this.props.match.params.id, 10);
            this.props.startLoading();
            const list = await this.props.getList(id);
            this.setState({list}, () => this.props.endLoading());
        }
    }

    handleSaveList = async (name: string, assignedTags: Tag[], moderators: number[]) => {
        const list = this.state.list as SharedList;
        this.props.startLoading();
        const updatedList = await this.props.updateList(list.id, name, assignedTags, moderators);
        if(updatedList) {
            let options = this.state.tagOptions as SelectListItem[];
            if(assignedTags.some(t => t.id.toString() === '0')) {
                const tags = await this.props.getTags();
                options = tags.map(o => {
                    return {
                        value: o.id,
                        text: o.name
                    };
                });
            }
            this.setState({list: updatedList, isInEditMode: false, tagOptions: options}, () => this.props.endLoading());
        } else {
            this.props.endLoading();
        }
    }

    switchToEditMode = async () => {
        this.props.startLoading();
        const tags = await this.props.getTags();
        const users = await this.props.getUsers();
        if(tags && users) {
            this.setState({
                isInEditMode: true,
                tagOptions: tags.map(t => {
                    return {
                        text: t.name,
                        value: t.id
                    };
                }),
                moderatorOptions: users.map(u => {
                    return {
                        text: u.login,
                        value: u.id
                    };
                })
            }, () => this.props.endLoading());
        } else {
            this.setState({
                isInEditMode: true
            }, () => this.props.endLoading());
        }

    }

    switchToSimpleMode = () => {
        this.setState({
            isInEditMode: false,
            moderatorOptions: null,
            tagOptions: null
        });
    }

    renderLegend = () => {
        if (this.state.list) {
            if(this.state.isInEditMode) {
                return (
                    <SharedListEditForm
                        tags={this.state.list.tags}
                        moderators={this.state.list.moderators}
                        moderatorsOptions={this.state.moderatorOptions as SelectListItem[]}
                        name={this.state.list.name}
                        tagsOptions={this.state.tagOptions as SelectListItem[]}
                        onSave={this.handleSaveList}
                        onCancel={this.switchToSimpleMode}
                    />
                );
            }
            return (
                <SharedListLegend
                    moderators={this.state.list.moderators}
                    name={this.state.list.name}
                    tags={this.state.list.tags}
                />
            );
        } else {
            return '';
        }
    }

    deleteItem = (item: SharedBookListItem) => {
        const that = this;
        return async function () {
            closeContextMenues();
            const confirmDeleting = confirm(
                `Do you really want to delete the item "${item.title}" by ${item.author}`);

            if (confirmDeleting) {
                if(that.state.list) {
                    that.props.startLoading();
                    const itemId = await that.props.removeItem(that.state.list.id, item.id);
                    if(itemId) {
                        const copy = cloneDeep(that.state.list);
                        const index = copy.items.findIndex(i => i.id === itemId);
                        copy.items.splice(index, 1);
                        that.setState({
                            list: copy
                        }, () => that.props.endLoading());
                    } else {
                        that.props.endLoading();
                    }
                }
            } else {
                return;
            }
        };
    }

    mapItem = (item: SharedBookListItem) => {
        const actions = [
            {
                onClick: () => this.showShareBookForm(item.bookId),
                text: 'Share'
            }
        ];
        if(this.state.list && this.state.list.canBeModerated) {
            actions.push(
                {
                    text: 'Delete',
                    onClick: this.deleteItem(item)
                },
            );
        }
        const Contexed = withContextMenu(actions, SharedBookLI);
        return <Contexed key={item.id} item={item} onDelete={this.deleteItem(item)} />;
    }

    closeForm = (event: React.MouseEvent<HTMLButtonElement>) => {
        this.setState({
            isFormHidden: true,
            books: null,
            bookSearchQuery: null
        });
    }

    showBooksForm = async () => {
        this.props.startLoading();
        const chunked = await this.props.findBooks('', 1);
        if(chunked) {
            this.setState({
                books: chunked.items,
                hasNext: chunked.hasNext,
                hasPrevious: chunked.hasPrevious,
                isFormHidden: false
            }, () => this.props.endLoading());
        } else {
            this.setState({
                isFormHidden: false
            }, () => this.props.endLoading());
        }
    }

    handleSearchChange = async (query: string) => {
        this.props.startLoading();
        const chunked = await this.props.findBooks(query, 1);
        if(chunked) {
            this.setState({
                books: chunked.items,
                hasNext: chunked.hasNext,
                hasPrevious: chunked.hasPrevious,
                chunk: chunked.chunk,
                bookSearchQuery: query
            }, () => this.props.endLoading());
        } else {
            this.props.endLoading();
        }
    }

    handleAddBook = async (id: number) => {
        if (this.state.list) {
            this.props.startLoading();
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
                        () => this.props.endLoading()
                    );
                }
            } else {
                this.setState(
                    {
                        books: null,
                        isFormHidden: true
                    },
                    () => this.props.endLoading()
                );
            }
        }
    }

    showShareBookForm = async (bookId: number) => {
        this.props.startLoading();
        const moderatedLists = await this.props.getModeratedLists();
        if(moderatedLists) {
            this.setState({
                shareBookFormHidden: false,
                moderatedLists,
                sharingBookId: bookId
            }, () => this.props.endLoading());
        } else {
            this.setState({
                shareBookFormHidden: false,
                sharingBookId: bookId
            }, () => this.props.endLoading());
        }
    }

    handleShareBook = async (bookId: number, listsIds: number[]) => {
        this.props.startLoading();
        await this.props.shareBook(bookId, listsIds);
        this.setState({
            moderatedLists: null,
            shareBookFormHidden: true,
            sharingBookId: null
        }, () => this.props.endLoading());
    }

    handleCancelSharingBook = () => {
        this.setState({
            shareBookFormHidden: true,
            sharingBookId: null
        });
    }

    handlePaging = async (newChunkNumber: number) => {
        this.props.startLoading();
        const query = this.state.bookSearchQuery as string;
        const chunked = await this.props.findBooks(query, newChunkNumber);
        if(chunked) {
            this.setState({
                books: chunked.items,
                hasNext: chunked.hasNext,
                hasPrevious: chunked.hasPrevious,
                chunk: chunked.chunk
            }, () => this.props.endLoading());
        } else {
            this.setState({
                books: null,
                bookSearchQuery: null,
                chunk: 1,
                hasNext: false,
                hasPrevious: false
            }, () => this.props.endLoading());
        }
    }

    handleNext = async () => {
        const chunk = this.state.chunk + 1;
        this.handlePaging(chunk);
    }

    handlePrevious = async () => {
        const chunk = this.state.chunk - 1;
        this.handlePaging(chunk);
    }

    removeList = async () => {
        const confirmDeleting = confirm(
            `Do you really want to delete the item this list?`);

        if (confirmDeleting) {
            if(this.state.list) {
                this.props.startLoading();
                await this.props.removeList(this.state.list.id);
                this.props.history.replace('/shared/search');
                this.props.endLoading();
            }
        } else {
            return;
        }
    }

    render() {
        let actions = [];

        if(this.state.list && this.state.list.editable) {
            actions.push({
                text: 'Edit',
                onClick: async () => await this.switchToEditMode()
            });
        }

        let Contexed = withContextMenu(actions, BookList);

        if (this.state.list) {
            return (
                <>
                    <Contexed items={this.state.list.items.map(this.mapItem)} legend={this.renderLegend()} />
                    {
                        this.state.list.canBeModerated && (
                            <>
                                <FixedGroup>
                                    <RoundButton
                                        radius={3}
                                        title="Add book"
                                        onClick={this.showBooksForm}
                                    >
                                        <i className="fas fa-book" />
                                    </RoundButton>
                                    {
                                        this.state.list.editable
                                            ? (
                                                <RoundButton
                                                    radius={3}
                                                    title="Delete"
                                                    onClick={this.removeList}
                                                >
                                                    <i className="fas fa-trash" />
                                                </RoundButton>
                                            )
                                            : null
                                    }
                                </FixedGroup>
                                {
                                    !this.state.isFormHidden &&
                                        <AddBookForm
                                            hasNext={this.state.hasNext}
                                            hasPrevious={this.state.hasPrevious}
                                            onNext={this.handleNext}
                                            onPrevious={this.handlePrevious}
                                            books={this.state.books ? this.state.books : []}
                                            searchQuery={this.state.bookSearchQuery ? this.state.bookSearchQuery : ''}
                                            onSubmit={this.handleAddBook}
                                            onCancel={this.closeForm}
                                            onQueryChange={this.handleSearchChange}
                                        />
                                }
                            </>
                        )
                    }
                    {
                        !this.state.shareBookFormHidden &&
                            <SharedBookForm
                                options={this.state.moderatedLists ? this.state.moderatedLists : []}
                                onSubmit={this.handleShareBook}
                                onCancel={this.handleCancelSharingBook}
                                choosenBookId={this.state.sharingBookId}
                            />
                    }
                </>
            );
        }
        return null;
    }
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
        getTags: async () => {
            const result = await new TagsService().getTags();
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        getUsers: async () => {
            const result = await new UsersService().getUsers();
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        updateList: async (id: number, name: string, tags: Tag[], moderators: number[]) => {
            const result = await listService.updateList({
                id,
                name,
                tags,
                moderators
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
        removeItem: async (listId: number, itemId: number) => {
            const result = await listService.removeItem(listId, itemId);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        startLoading: () => {
            dispatch(loadingActions.start());
        },
        endLoading: () => {
            dispatch(loadingActions.end());
        },
        findBooks: async (query: string, chunk: number | null) => {
            const result = await bookService.findBooks(query, chunk);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        shareBook: async (bookId: number, lists: number[]) => {
            const result = await bookService.shareBook(bookId, lists);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
        },
        getModeratedLists: async () => {
            const result = await new ListsService().getModeratedLists();
            if(!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data;
        },
        removeList: async (listId: number) => {
            const result = await listService.removeList(listId);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
        }
    };
}

export default connect(null, mapDispatchToProps)(SharedBookList);