import * as React from 'react';
import { Form, SimpleSearch, Pagination, Grid } from '../../controls';
import { Book } from '../../models';
import { BookGridItem } from '../BookGridItem';

interface Props {
    onSubmit: (id: number) => void;
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
    searchQuery?: string;
    onQueryChange: (query: string) => void;
    books: Book[];
    hasPrevious: boolean;
    hasNext: boolean;
    onNext: () => void;
    onPrevious: () => void;
}

interface State {
    bookId: number | null;
}

class AddBookForm extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = { bookId: null };
    }

    renderBooks = (book: Book) => {
        return (
            <BookGridItem
                key={book.id}
                bookId={book.id}
                header={`${book.title}\n\r${book.author}`}
                tags={book.tags}
                genre={book.genre}
                selected={book.id === this.state.bookId}
                onItemClick={this.handleGridItemClick}
            />
        );
    }

    handleGridItemClick = (bookId: number) => {
        this.setState({
            bookId
        });
    }

    unselectBook = () => {
        this.setState({
            bookId: null
        });
    }

    handleSearchChange = (query: string) => {
        this.unselectBook();
        this.props.onQueryChange(query);
    }

    handleFormCancel = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onCancel(event);
    }

    handleFormSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if(this.state.bookId) {
            this.props.onSubmit(this.state.bookId);
        }
    }

    render() {
        return (
            <Form
                header={'Add book'}
                size={
                    {
                        height: '98%',
                        width: '90%'
                    }
                }
                onSubmit={this.handleFormSubmit}
                onCancel={this.handleFormCancel}
            >
                <SimpleSearch
                    onChange={this.handleSearchChange}
                    query={this.props.searchQuery}
                />
                <Pagination
                    hasNext={this.props.hasNext}
                    hasPrevious={this.props.hasPrevious}
                    onNext={this.props.onNext}
                    onPrevious={this.props.onPrevious}
                >
                    <Grid items={this.props.books.map(this.renderBooks)} />
                </Pagination>
            </Form>
        );
    }
}

export default AddBookForm;