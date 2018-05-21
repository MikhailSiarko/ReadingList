import * as React from 'react';
import styles from './BookLI.css';
import Layout from '../../Layout/Layout';
import { BookStatusKey } from '../../../models/BookList/Implementations/BookStatus';

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
    onItemChange: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    options?: JSX.Element[];
    isInEditMode: boolean;
}
class BookLI extends React.Component<BookLIProps> {
    constructor(props: BookLIProps) {
        super(props);
        this.state = { isInEditMode: false };
    }
    render() {
        if(this.props.isInEditMode) {
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
                            <select defaultValue={this.props.listItem.status}
                                    name="BookStatus" data-item-id={this.props.listItem.id}>
                                {this.props.options}
                            </select>
                        </div>
                        : null
                }
            </Layout>
            );
        }
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
                            <p>Status: {BookStatusKey[this.props.listItem.status]}</p>
                        </div>
                        : null
                }
            </Layout>
        );
    }
}

export default BookLI;