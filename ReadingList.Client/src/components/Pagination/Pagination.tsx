import * as React from 'react';
import styles from './Pagination.css';
import globalStyles from '../../styles/global.css';
import Colors from '../../styles/colors';
import RoundButton from '../RoundButton';

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
                    <div>
                        <RoundButton
                            radius={3}
                            title="Previous"
                            className={globalStyles.shadowed}
                            style={{backgroundColor: Colors.Primary}}
                            disabled={!this.props.hasPrevious}
                            onClick={this.props.onPrevious}
                        ><i className="fas fa-arrow-left" /></RoundButton>
                    </div>
                    <div>
                        {this.props.children}
                    </div>
                    <div>
                        <RoundButton
                            radius={3}
                            title="Next"
                            className={globalStyles.shadowed}
                            style={{backgroundColor: Colors.Primary}}
                            disabled={!this.props.hasNext}
                            onClick={this.props.onNext}
                        ><i className="fas fa-arrow-right" /></RoundButton>
                    </div>
                </div>
            </div>
        );
    }
}

export default Pagination;