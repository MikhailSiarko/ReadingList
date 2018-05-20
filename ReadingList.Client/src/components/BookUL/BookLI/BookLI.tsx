import * as React from 'react';
import styles from './BookLI.css';
import Layout from '../../Layout/Layout';

interface BookLIProps {
    element?: string;
    listItem: {
        id: string;
        data: {
            title: string;
            author: string;
        }
        status: string;
    };
    shouldStatusSelectorRender: boolean;
    onBookStatusChange: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    options?: JSX.Element[];
}

class BookLI extends React.Component<BookLIProps> {
    render() {
        return (
            <Layout element={this.props.element ? this.props.element : 'li'} className={styles['book-li']}>
                    <div className={styles['book-info']}>
                        <h5 className={styles['book-title']}>
                            <q>{this.props.listItem.data.title}</q> by {this.props.listItem.data.author}
                        </h5>
                    </div>
                {
                    this.props.shouldStatusSelectorRender
                        ?
                        <div className={styles['status-selector']}>
                            <p>Status:</p>
                            <select value={this.props.listItem.status}
                                    name="BookStatus" data-item-id={this.props.listItem.id}
                                    onChange={this.props.onBookStatusChange}>
                                {this.props.options}
                            </select>
                        </div>
                        : null
                }
            </Layout>
        );
    }
}

export default BookLI;