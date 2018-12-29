import * as React from 'react';
import { Form } from '../Form';
import { NamedValue, Book } from '../../models';
import SimpleSearch from '../SimpleSearch';
import Grid from '../Grid';
import BookGridItem from '../Grid/BookGridItem';
import bookItemStyles from '../Grid/BookGridItem/BookGridItem.css';

interface Props {
    hidden: boolean;
    onSubmit: (id: number) => Promise<void>;
    onCancel: (event: React.MouseEvent<HTMLButtonElement>) => void;
    searchQuery?: string;
    onQueryChange: (query: string) => void;
    books: Book[];
}

class AddBookForm extends React.Component<Props> {
    renderBooks = (book: Book) => {
        return (
            <BookGridItem
                key={book.id}
                bookId={book.id}
                header={`${book.title}\n\r${book.author}`}
                tags={book.tags}
                genre={book.genre}
                onClick={this.handleGridItemClick}
            />
        );
    }

    handleGridItemClick = (event: React.MouseEvent<HTMLDivElement>) => {
        event.preventDefault();
        const item = event.currentTarget;
        const grid = item.parentElement as HTMLElement;
        const input = document.querySelector('input[name="selected-book"]') as HTMLInputElement;
        if(item.classList.contains(bookItemStyles['selected-book-grid-item'])) {
            this.resetBookInput();
            this.unselectBook(grid);
        } else {
            this.unselectBook(grid);
            item.classList.add(bookItemStyles['selected-book-grid-item']);
            input.value = item.dataset.bookId as string;
        }
    }

    unselectBook = (grid: HTMLElement) => {
        const items = grid.getElementsByClassName(bookItemStyles['selected-book-grid-item']);
        Array.from(items).forEach(i => {
            i.classList.remove(bookItemStyles['selected-book-grid-item']);
        });
    }

    handleSearchChange = async (query: string) => {
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

    handleFormSubmit = async (values: NamedValue[]) => {
        const idValue = values.filter(v => v.name === 'selected-book')[0].value;
        if(idValue) {
            const id = parseInt(values.filter(v => v.name === 'selected-book')[0].value, 10);
            await this.props.onSubmit(id);
        } else {
            alert('A book isn\'t chosen!');
        }

    }

    render() {
        return (
            <Form
                header={'Add book'}
                size={
                    {
                        height: '90%',
                        width: '80%'
                    }
                }
                hidden={this.props.hidden}
                onSubmit={this.handleFormSubmit}
                onCancel={this.handleFormCancel}
            >
                <input type="text" name="selected-book" hidden={true} defaultValue="" />
                <SimpleSearch
                    onChange={this.handleSearchChange}
                    query={this.props.searchQuery}
                />
                <Grid items={this.props.books.map(this.renderBooks)} />
            </Form>
        );
    }
}

export default AddBookForm;