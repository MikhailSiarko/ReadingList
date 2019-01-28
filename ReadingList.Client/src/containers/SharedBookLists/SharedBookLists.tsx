import * as React from 'react';
import SimpletSearch from '../../components/SimpleSearch';
import Grid from '../../components/Grid';
import { NamedValue, SelectListItem, SharedBookListPreview, Chunked } from '../../models';
import { SharedBookListService } from '../../services';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { createPropActionWithResult, processFailedRequest } from '../../utils';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import { Form } from '../../components/Form';
import { TagsService } from '../../services/TagsService';
import ListGridItem from '../../components/Grid/ListGridItem';
import { withContextMenu } from '../../hoc';
import CreateSharedList from '../../components/CreateSharedList';
import { Tag } from '../../models/Tag';
import FixedGroup from '../../components/FixedGroup';
import RoundButton from '../../components/RoundButton';
import { parse } from 'query-string';
import Pagination from '../../components/Pagination';
import { Constants } from '../../models/Constants';

interface Props extends RouteComponentProps<any> {
    getSharedLists: (query: string, chunk: number | null, count: number | null) =>
        Promise<Chunked<SharedBookListPreview>>;
    getOwnSharedLists: (chunk: number | null, count: number | null) => Promise<Chunked<SharedBookListPreview>>;
    createList: (data: { name: string, tags: Tag[] }) => Promise<SharedBookListPreview>;
    getTags: () => Promise<Tag[]>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

interface State {
    hasPrevious: boolean;
    hasNext: boolean;
    chunk: number;
    sharedLists: SharedBookListPreview[] | null;
    tags: SelectListItem[] | null;
    isFormHidden: boolean;
    dataFetched: boolean;
}

class SharedBookLists extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            hasPrevious: false,
            hasNext: false,
            chunk: 1,
            sharedLists: null,
            isFormHidden: true,
            tags: null,
            dataFetched: false
        };
    }

    searchHandler = async (query: string) => {
        const search = this.getSearch();
        this.props.history.push(
            '/shared/search?query=' + encodeURIComponent(query) + '&chunk=' + search.chunkNumber,
            {from: this.props.location}
        );
    }

    async componentDidMount() {
        this.props.loadingStart();
        const { query, chunk, count } = parse(this.props.location.search);
        let chunked;
        if(query === 'own') {
            chunked = await this.props.getOwnSharedLists(
                chunk ? parseInt(chunk as string, 10) : null,
                count ? parseInt(count as string, 10) : null
            );
        } else {
            chunked = await this.props.getSharedLists(
                query ? decodeURIComponent(query as string) : '',
                chunk ? parseInt(chunk as string, 10) : null,
                count ? parseInt(count as string, 10) : null
            );
        }

        let tags = await this.props.getTags();

        if(chunked && tags) {
            this.setState(
                {
                    sharedLists: chunked.items,
                    hasNext: chunked.hasNext,
                    hasPrevious: chunked.hasPrevious,
                    chunk: chunked.chunk,
                    tags: tags.map(t => {
                        return {
                            text: t.name,
                            value: t.id
                        };
                    }),
                    dataFetched: true
                },
                () => this.props.loadingEnd()
            );
        }
    }

    async componentDidUpdate(prevProps: Props) {
        const prevSearch = parse(prevProps.location.search);
        const currentSearch = parse(this.props.location.search);
        if(prevSearch.query !== currentSearch.query || prevSearch.chunk !== currentSearch.chunk) {
            this.props.loadingStart();
            const { query, chunk, count } = parse(this.props.location.search);
            let chunked;
            if(query === 'own') {
                chunked = await this.props.getOwnSharedLists(
                    chunk ? parseInt(chunk as string, 10) : null,
                    count ? parseInt(count as string, 10) : Constants.ITEMS_PER_PAGE
                );
            } else {
                chunked = await this.props.getSharedLists(
                    query ? decodeURIComponent(query as string) : '',
                    chunk ? parseInt(chunk as string, 10) : null,
                    count ? parseInt(count as string, 10) : Constants.ITEMS_PER_PAGE
                );
            }
            this.setState(
                {
                    sharedLists: chunked.items,
                    chunk: chunked.chunk,
                    hasNext: chunked.hasNext,
                    hasPrevious: chunked.hasPrevious
                },
                () => this.props.loadingEnd()
            );
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
        const tags = values.filter(item => item.name === 'tags')[0].value as SelectListItem[];
        const list = await this.props.createList(
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

        const lists = await this.props.getSharedLists(
            searchData.query,
            searchData.chunkNumber,
            Constants.ITEMS_PER_PAGE
        );

        if (list && lists) {
            this.setState(
                {
                    sharedLists: lists.items,
                    hasPrevious: lists.hasPrevious,
                    hasNext: lists.hasNext,
                    chunk: lists.chunk,
                    isFormHidden: true
                },
                () => this.props.loadingEnd()
            );
        } else {
            this.setState({isFormHidden: true}, () => this.props.loadingEnd());
        }
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

    render() {
        if (this.state.dataFetched) {
            const items = (this.state.sharedLists as SharedBookListPreview[]).map(this.mapList);
            return (
                <>
                    <SimpletSearch
                        query={parse(this.props.location.search).query as string}
                        onChange={this.searchHandler}
                    />
                    <Pagination
                        onNext={this.handleNext}
                        onPrevious={this.handlePrevious}
                        hasNext={this.state.hasNext}
                        hasPrevious={this.state.hasPrevious}
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
                                <CreateSharedList tags={this.state.tags as SelectListItem[]} />
                            </Form>
                    }
                </>
            );
        }
        return null;
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>) {
    const bookService = new SharedBookListService();
    return {
        getSharedLists: async (query: string, chunk: number | null, count: number | null) => {
            const result = await bookService.getLists(query, chunk, count);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data as Chunked<SharedBookListPreview>;
        },
        getOwnSharedLists: async (chunk: number | null, count: number | null) => {
            const result = await bookService.getOwnLists(chunk, count);
            if (!result.isSucceed) {
                processFailedRequest(result, dispatch);
            }
            return result.data as Chunked<SharedBookListPreview>;
        },
        createList: createPropActionWithResult(bookService.createList, dispatch),
        getTags: createPropActionWithResult(new TagsService().getTags, dispatch),
        loadingStart: () => {
            dispatch(loadingActions.start());
        },
        loadingEnd: () => {
            dispatch(loadingActions.end());
        }
    };
}

export default connect(null, mapDispatchToProps)(SharedBookLists);