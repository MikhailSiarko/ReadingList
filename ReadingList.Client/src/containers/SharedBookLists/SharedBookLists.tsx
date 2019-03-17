import * as React from 'react';
import SimpletSearch from '../../components/SimpleSearch';
import Grid from '../../components/Grid';
import { NamedValue, SelectListItem, SharedBookListPreview, Chunked, Tag, SharedListCreateData } from '../../models';
import { Dispatch } from 'redux';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Form } from '../../components/Form';
import ListGridItem from '../../components/Grid/ListGridItem';
import { withContextMenu } from '../../hoc';
import CreateSharedList from '../../components/CreateSharedList';
import FixedGroup from '../../components/FixedGroup';
import RoundButton from '../../components/RoundButton';
import { parse } from 'querystring';
import Pagination from '../../components/Pagination';
import { RootState } from '../../store';
import { sharedListActions } from '../../store/sharedList';
import { tagActions } from '../../store';

interface Props extends RouteComponentProps<any> {
    lists: Chunked<SharedBookListPreview> | null;
    tags: Tag[] | null;
    fetchSharedLists: (query: string, chunk: number | null) => void;
    fetchMySharedLists: (chunk: number | null) => void;
    createList: (data: SharedListCreateData) => void;
    fetchTags: () => void;
    clearTagsState: () => void;
}

interface State {
    isFormHidden: boolean;
}

class SharedBookLists extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            isFormHidden: true
        };
    }

    searchHandler = (query: string) => {
        this.props.history.push(
            '/shared/search?query=' + encodeURIComponent(query) + '&chunk=' + 1,
            {from: this.props.location}
        );
    }

    fetchLists = () => {
        const { query, chunk } = parse(this.props.location.search);
        this.props.fetchSharedLists(
            query ? decodeURIComponent(query as string) : '',
            chunk ? parseInt(chunk as string, 10) : null
        );
    }

    componentDidMount() {
        this.fetchLists();
    }

    componentDidUpdate(prevProps: Props) {
        const prevSearch = parse(prevProps.location.search);
        const currentSearch = parse(this.props.location.search);
        if(prevSearch.query !== currentSearch.query || prevSearch.chunk !== currentSearch.chunk) {
            this.fetchLists();
        }
    }

    handleButtonClick = () => {
        this.props.fetchTags();
        this.setState({isFormHidden: false});
    }

    handleCancel = () => {
        this.setState({isFormHidden: true});
        this.props.clearTagsState();
    }

    handleListFormSubmit = (values: NamedValue[]) => {
        const name = values.filter(item => item.name === 'name')[0].value;
        const tags = values.filter(item => item.name === 'tags')[0].value as SelectListItem[];
        this.props.createList(
            {
                name,
                tags: tags.map(i => {
                    return {
                        id: i.value,
                        name: i.text
                    };
                })
            }
        );

        const searchData = this.getSearch();

        this.props.fetchSharedLists(
            searchData.query,
            searchData.chunkNumber
        );

        this.setState({isFormHidden: true});
    }

    mapList = (list: SharedBookListPreview) => {
        const openList = () => this.props.history.push(
            '/shared/' + list.id,
            {from: this.props.location}
        );
        let actions = [
            {
                text: 'Open',
                onClick: openList
            }
        ];

        let Contexed = withContextMenu(actions, ListGridItem);

        return (
            <Contexed
                key={list.id}
                header={list.name}
                tags={list.tags}
                booksCount={list.booksCount}
                onClick={openList}
            />
        );
    }

    getSearch = () => {
        let { query, chunk } = parse(this.props.location.search);
        query = query ? query as string : '';
        let chunkNumber = chunk ? parseInt(chunk as string, 10) : 1;
        return {query, chunkNumber};
    }

    handlePaging = (stepSign: number) => {
        const search = this.getSearch();
        this.props.history.push(
            `/shared/search?query=${encodeURIComponent(search.query)}&chunk=${search.chunkNumber + (stepSign)}`,
            {from: this.props.location}
        );
    }

    handleNext = () => {
        this.handlePaging(1);
    }

    handlePrevious = () => {
        this.handlePaging(-1);
    }

    mapTag(tag: Tag): SelectListItem {
        return {
            text: tag.name,
            value: tag.id
        };
    }

    render() {
        if (this.props.lists) {
            const items = (this.props.lists.items as SharedBookListPreview[]).map(this.mapList);
            return (
                <>
                    <SimpletSearch
                        query={parse(this.props.location.search).query as string}
                        onChange={this.searchHandler}
                    />
                    <Pagination
                        onNext={this.handleNext}
                        onPrevious={this.handlePrevious}
                        hasNext={this.props.lists.chunkInfo.hasNext}
                        hasPrevious={this.props.lists.chunkInfo.hasPrevious}
                    >
                        <Grid items={items} />
                    </Pagination>
                    <FixedGroup>
                        <RoundButton radius={3} title="Create new list" onClick={this.handleButtonClick}>
                            <i className="fas fa-list-alt" />
                        </RoundButton>
                    </FixedGroup>
                    {
                        !this.state.isFormHidden &&
                            <Form
                                header={'Add new list'}
                                onSubmit={this.handleListFormSubmit}
                                onCancel={this.handleCancel}
                            >
                                <CreateSharedList
                                    tags={
                                        this.props.tags
                                            ? this.props.tags.map(this.mapTag)
                                            : null
                                    }
                                />
                            </Form>
                    }
                </>
            );
        }
        return null;
    }
}

function mapStateToProps(state: RootState) {
    return {
        tags: state.tags,
        lists: state.shared.lists
    };
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    return {
        fetchSharedLists: (query: string, chunk: number | null) => {
            dispatch(sharedListActions.fetchListsBegin(query, chunk));
        },
        fetchMySharedLists: (chunk: number | null) => {
            dispatch(sharedListActions.fetchListsBegin('my', chunk));
        },
        createList: (data: SharedListCreateData) => {
            dispatch(sharedListActions.createListBegin(data));
        },
        fetchTags: () => {
            dispatch(tagActions.fetchTagBegin());
        },
        clearTagsState: () => {
            dispatch(tagActions.clearTagsState());
        }
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(SharedBookLists);