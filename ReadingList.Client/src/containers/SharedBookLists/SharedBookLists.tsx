import * as React from 'react';
import SharedListSearch from '../../components/SharedListSearch';
import Grid from '../../components/Grid';
import { NamedValue, SelectListItem, SharedBookListPreview } from '../../models';
import { SharedBookListService } from '../../services';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { createPropActionWithResult } from '../../utils';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { loadingActions } from '../../store/actions/loading';
import { Form } from '../../components/Form';
import FixedButton from '../../components/FixedButton';
import { TagsService } from '../../services/TagsService';
import ListGridItem from '../../components/Grid/ListGridItem';
import { withContextMenu } from '../../hoc';
import CreateSharedList from '../../components/CreateSharedList';
import { Tag } from '../../models/Tag';

interface Props extends RouteComponentProps<any> {
    getSharedLists: (query: string) => Promise<SharedBookListPreview[]>;
    getOwnSharedLists: () => Promise<SharedBookListPreview[]>;
    createList: (data: { name: string, tags: Tag[] }) => Promise<SharedBookListPreview>;
    getTags: () => Promise<Tag[]>;
    loadingStart: () => void;
    loadingEnd: () => void;
}

interface State {
    sharedLists: SharedBookListPreview[] | null;
    tags: SelectListItem[] | null;
    isFormHidden: boolean;
    dataFetched: boolean;
}

class SharedBookLists extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            sharedLists: null,
            isFormHidden: true,
            tags: null,
            dataFetched: false
        };
    }

    searchHandler = async (query: string) => {
        this.props.history.push('/shared/search/' + encodeURIComponent(query), {from: this.props.location});
    }

    async componentDidMount() {
        this.props.loadingStart();

        const query = this.props.match.params.query ? this.props.match.params.query : '';
        let lists = await this.props.getSharedLists(decodeURIComponent(query));

        let tags = await this.props.getTags();

        if(lists && tags) {
            this.setState(
                {
                    sharedLists: lists,
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
        if(prevProps.match.params.query !== this.props.match.params.query) {
            this.props.loadingStart();
            const query = this.props.match.params.query ? this.props.match.params.query : '';
            const lists = await this.props.getSharedLists(decodeURIComponent(query));
            this.setState({sharedLists: lists}, () => this.props.loadingEnd());
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
        const copies = [...(this.state.sharedLists as SharedBookListPreview[])];
        if (copies && list) {
            copies.push(list);
            this.setState({sharedLists: copies, isFormHidden: true}, () => this.props.loadingEnd());
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

    render() {
        if (this.state.dataFetched) {
            const items = (this.state.sharedLists as SharedBookListPreview[]).map(this.mapList);
            return (
                <>
                    <SharedListSearch query={this.props.match.params.query} onSubmit={this.searchHandler} />
                    <Grid items={items} />
                    <FixedButton radius={3} title="Create new list" onClick={this.handleButtonClick}>+</FixedButton>
                    <Form
                        header={'Add new list'}
                        hidden={this.state.isFormHidden}
                        onSubmit={this.handleListFormSubmit}
                        onCancel={this.handleCancel}
                    >
                        <CreateSharedList tags={this.state.tags as SelectListItem[]} />
                    </Form>
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