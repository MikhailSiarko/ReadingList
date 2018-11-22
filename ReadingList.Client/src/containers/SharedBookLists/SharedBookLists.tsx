import * as React from 'react';
import SharedListSearch from '../../components/SharedListSearch';
import Grid from '../../components/Grid';
import { SharedBookList } from '../../models';
import { SharedBookListService } from '../../services';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { createPropActionWithResult, reduceTags } from '../../utils';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import { cloneDeep } from 'lodash';
import { NamedValue, AddForm } from '../../components/AddForm';
import FixedButton from '../../components/FixedButton';

interface Props extends RouteComponentProps<any> {
    loading: boolean;
    getSharedLists: (query: string) => Promise<SharedBookList[]>;
    getOwnSharedLists: () => Promise<SharedBookList[]>;
    createList: (data: {name: string, tags: string[]}) => Promise<SharedBookList>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

interface State {
    sharedLists: SharedBookList[] | null;
    isFormHidden: boolean;
}

class SharedBookLists extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {sharedLists: null, isFormHidden: true};
    }

    searchHandler = async (query: string) => {
        this.props.loadingStart();
        const lists = await this.props.getSharedLists(query);
        this.props.loadingEnd();
        this.setState({sharedLists: lists});
    }

    async componentDidMount() {
        if(this.state.sharedLists === null) {
            this.props.loadingStart();
            let lists = await this.props.getSharedLists('');
            this.props.loadingEnd();
            this.setState({sharedLists: lists});
        }
    }

    shouldComponentUpdate(_: Props, nextState: State) {
        return nextState.sharedLists !== null;
    }

    handleButtonClick = () => {
        this.setState({isFormHidden: false});
    }

    handleCancel = () => {
        this.setState({isFormHidden: true});
    }

    handleListFormSubmit = async (values: NamedValue[]) => {
        this.props.loadingStart();
        const name = values.filter(item => item.name === 'name')[0].value;
        const tags = values.filter(item => item.name === 'tags')[0].value.replace(' ', '').split(',');
        const list = await this.props.createList({name, tags});
        this.props.loadingEnd();
        const copies = cloneDeep(this.state.sharedLists);
        if(copies) {
            copies.push(list);
            this.setState({sharedLists: copies, isFormHidden: true});
        }
    }

    componentDidUpdate() {
        this.props.loadingEnd();
    }

    mapList = (list: SharedBookList) => {
        return {
            header: list.name,
            content: (
                <div>
                    <p>{reduceTags(list.tags)}</p>
                    <h4>{list.booksCount} book(s)</h4>
                </div>
            ),
            onClick: () => this.props.history.push('/shared/' + list.id,
                {from: this.props.location})
        };
    }

    render() {
        if(this.state.sharedLists) {
                const items = this.state.sharedLists.map(this.mapList);
                return (
                    <div>
                        <SharedListSearch query={this.props.match.params.query} onSubmit={this.searchHandler} />
                        <Grid items={items} />
                        <FixedButton radius={3} onClick={this.handleButtonClick}>+</FixedButton>
                        <AddForm
                            header={'Add new list'}
                            inputs={[
                                {name: 'name', type: 'text', required: true, placeholder: 'Enter the name...' },
                                {name: 'tags', type: 'text', required: true, placeholder: 'Enter the tags...' }
                            ]}
                            isHidden={this.state.isFormHidden}
                            onSubmit={this.handleListFormSubmit}
                            onCancel={this.handleCancel}
                        />
                    </div>
                );
            }
            return null;
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
        getSharedLists: createPropActionWithResult(bookService.getLists, dispatch),
        getOwnSharedLists: createPropActionWithResult(bookService.getOwnLists, dispatch),
        createList: createPropActionWithResult(bookService.createList, dispatch),
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SharedBookLists);