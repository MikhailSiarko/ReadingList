import * as React from 'react';
import { Form } from '../Form';
import { Book } from '../../models';
import SimpleSearch from '../SimpleSearch';
import Grid from '../Grid';
import BookGridItem from '../Grid/BookGridItem';
import bookItemStyles from '../Grid/BookGridItem/BookGridItem.scss';
import Pagination from '../Pagination';

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

    unselectBook = (grid: HTMLElement) => {
        const items = grid.getElementsByClassName(bookItemStyles['selected-book-grid-item']);
        Array.from(items).forEach(i => {
            i.classList.remove(bookItemStyles['selected-book-grid-item']);
        });
    }

    handleSearchChange = (query: string) => {
        const input = document.querySelector('input[name="selected-book"]') as HTMLInputElement;
        this.resetBookInput(input);
        const grid = (input.parentElement as HTMLElement).lastElementChild as HTMLElement;
        this.unselectBook(grid);
        this.props.onQueryChange(query);
    }

    resetBookInput(input: HTMLInputElement | null = null) {
        if(!input) {
            input = document.querySelector('input[name="selected-book"]') as HTMLInputElement;
        }
        input.value = '';
    }

    handleFormCancel = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        if(event.currentTarget.form) {
            Array.from(event.currentTarget.form.elements).forEach(i => {
                if(i.tagName === 'INPUT') {
                    const input = i as HTMLInputElement;
                    input.value = '';
                }
            });
        }
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
                <input type="text" name="selected-book" hidden={true} defaultValue="" />
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