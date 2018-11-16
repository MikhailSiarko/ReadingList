import * as React from 'react';
import { SharedBookList as SharedList } from '../../models/BookList/Implementations/SharedBookList';
import { SharedBookListService } from '../../services';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import BookList from '../../components/BookList';
import SharedBookLI from '../../components/SharedBookLI/SharedBookLI';
import { withSpinner } from '../../hoc';
import { AddForm, NamedValue } from '../../components/AddForm';
import { cloneDeep } from 'lodash';
import { SharedBookListItem } from '../../models/BookList';
import FixedButton from '../../components/FixedButton';

interface Props extends RouteComponentProps<any> {
    loading: boolean;
    getList: (id: number) => Promise<SharedList>;
    addItem: (listId: number, item: SharedBookListItem) => Promise<SharedBookListItem>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

interface State {
    list: SharedList | null;
    isFormHidden: boolean;
}

class SharedBookList extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {list: null, isFormHidden: true};
    }

    async componentDidMount() {
        if(this.state.list == null) {
            const id = parseInt(this.props.match.params.id, 10);
            this.props.loadingStart();
            const list = await this.props.getList(id);
            this.props.loadingEnd();
            this.setState({list});
        }
    }

    handleButtonClick = () => {
        this.setState({isFormHidden: false});
    }

    handleCancel = (_: React.MouseEvent<HTMLButtonElement>) => {
        this.setState({isFormHidden: true});
    }

    handleListFormSubmit = async (values: NamedValue[]) => {
        this.props.loadingStart();
        const title = values.filter(item => item.name === 'title')[0].value;
        const author = values.filter(item => item.name === 'author')[0].value;
        const listItem =
            await this.props.addItem((this.state.list as SharedList).id, new SharedBookListItem(author, title));
        this.props.loadingEnd();
        const copy = cloneDeep(this.state.list);
        if(copy) {
            copy.items.push(listItem);
            this.setState({list: copy, isFormHidden: true});
        }
    }

    render() {
        const Spinnered = withSpinner(this.state.list && !this.props.loading, () => {
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
                return (
                    <>
                        <BookList items={listItems} legend={legend} />
                        {
                            this.state.list && this.state.list.canEdit
                                ? (
                                    <>
                                        <FixedButton radius={3} onClick={this.handleButtonClick}>+</FixedButton>
                                        <AddForm
                                            header={'Add new item'}
                                            // TODO Replace author and title entering with searching
                                            inputs={[
                                                {
                                                    name: 'title',
                                                    type: 'text',
                                                    required: true,
                                                    placeholder: 'Enter the title...'
                                                },
                                                {
                                                    name: 'author',
                                                    type: 'text',
                                                    required: true,
                                                    placeholder: 'Enter the author...'
                                                }
                                            ]}
                                            isHidden={this.state.isFormHidden}
                                            onSubmit={this.handleListFormSubmit}
                                            onCancel={this.handleCancel}
                                        />
                                    </>
                                )
                                : null
                        }

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
    const bookService = new SharedBookListService();
    return {
        getList: async (id: number) => {
            const result = await bookService.getList(id);
            return result.data as SharedList;
        },
        addItem: async (listId: number, listItem: SharedBookListItem) => {
            const result = await bookService.addItem(listId, listItem);
            return result.data as SharedBookListItem;
        },
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SharedBookList);