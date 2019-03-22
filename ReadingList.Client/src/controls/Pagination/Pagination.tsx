import * as React from 'react';
import styles from './Pagination.scss';
import { RoundButton } from '../RoundButton';

interface Props {
    hasNext: boolean;
    hasPrevious: boolean;
    onNext: () => void;
    onPrevious: () => void;
}

class Pagination extends React.Component<Props> {
    render() {
        return (
            <div className={styles.pagination}>
                <div>
                    <RoundButton
                        radius={3}
                        title="Previous"
                        disabled={!this.props.hasPrevious}
                        onClick={this.props.onPrevious}
                    ><i className="fas fa-arrow-left" /></RoundButton>
                    <div>
                        {this.props.children}
                    </div>
                    <RoundButton
                        radius={3}
                        title="Next"
                        disabled={!this.props.hasNext}
                        onClick={this.props.onNext}
                    ><i className="fas fa-arrow-right" /></RoundButton>
                </div>
            </div>
        );
    }
}

export default Pagination;