import * as React from 'react';
import SharedListSearch from '../../components/SharedListSearch';
import Grid from '../../components/Grid';
import { SharedBookList, NamedValue, SelectListItem } from '../../models';
import { SharedBookListService } from '../../services';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { createPropActionWithResult } from '../../utils';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import { AddForm } from '../../components/AddForm';
import FixedButton from '../../components/FixedButton';
import MultiSelect from '../../components/MultiSelect';
import globalStyles from '../../styles/global.css';
import { TagsService } from '../../services/TagsService';
import GridItem from '../../components/Grid/GridItem';
import { withContextMenu } from '../../hoc';

interface Props extends RouteComponentProps<any> {
    getSharedLists: (query: string) => Promise<SharedBookList[]>;
    getOwnSharedLists: () => Promise<SharedBookList[]>;
    createList: (data: { name: string, tags: SelectListItem[] }) => Promise<SharedBookList>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

interface State {
    sharedLists: SharedBookList[] | null;
    tags: SelectListItem[] | null;
    isFormHidden: boolean;
}

class SharedBookLists extends React.PureComponent<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {sharedLists: null, isFormHidden: true, tags: null};
    }

    searchHandler = async (query: string) => {
        this.props.loadingStart();
        const lists = await this.props.getSharedLists(query);
        this.setState({sharedLists: lists}, () => this.props.loadingEnd());
    }

    async componentDidMount() {
        let lists: SharedBookList[] | null = null;
        let tags: SelectListItem[] | null = null;
        this.props.loadingStart();
        if (this.state.sharedLists === null) {
            lists = await this.props.getSharedLists('');
        }

        if(this.state.tags === null) {
            let result = await new TagsService().getTags();
            if(result.data) {
                tags = result.data;
            }
        }

        if(lists || tags) {
            this.setState({sharedLists: lists, tags: tags}, () => this.props.loadingEnd());
        }
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
        const tags = values.filter(item => item.name === 'tags')[0].value;
        const list = await this.props.createList({name, tags});
        const copies = [...(this.state.sharedLists as SharedBookList[])];
        if (copies && list) {
            copies.push(list);
            this.setState({sharedLists: copies, isFormHidden: true}, () => this.props.loadingEnd());
        } else {
            this.setState({isFormHidden: true}, () => this.props.loadingEnd());
        }
    }

    mapList = (list: SharedBookList) => {
        let actions = [
            {
                text: 'Open',
                onClick: () => this.props.history.push(
                    '/shared/' + list.id,
                    {from: this.props.location}
                )
            }
        ];

        let Contexed = withContextMenu(actions, GridItem);

        return (
            <Contexed
                key={list.id}
                header={list.name}
                tags={list.tags}
                booksCount={list.booksCount}
                onClick={
                    () => this.props.history.push(
                        '/shared/' + list.id,
                        {from: this.props.location}
                    )
                }
            />
        );
    }

    render() {
        if (this.state.sharedLists) {
            const items = this.state.sharedLists.map(this.mapList);
            return (
                <>
                    <SharedListSearch query={this.props.match.params.query} onSubmit={this.searchHandler} />
                    <Grid items={items} />
                    <FixedButton radius={3} title="Create new list" onClick={this.handleButtonClick}>+</FixedButton>
                    <AddForm
                        header={'Add new list'}
                        hidden={this.state.isFormHidden}
                        onSubmit={this.handleListFormSubmit}
                        onCancel={this.handleCancel}
                    >
                        <div>
                            <input
                                type="text"
                                name="name"
                                required={true}
                                placeholder="Enter the name..."
                                className={globalStyles.shadowed}
                            />
                        </div>
                        <div>
                            <MultiSelect
                                name="tags"
                                placeholder={'Select tags'}
                                options={this.state.tags as SelectListItem[]}
                                selectedFormat={item => `#${item.text}`}
                                addNewIfNotFound={true}
                            />
                        </div>
                    </AddForm>
                </>
            );
        }
        return null;
    }
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

export default connect(null, mapDispatchToProps)(SharedBookLists);