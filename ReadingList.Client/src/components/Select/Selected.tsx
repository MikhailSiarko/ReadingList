import * as React from 'react';
import { SelectListItem } from '../../models';
import styles from './Selected.css';

interface Props {
    item: SelectListItem;
    onRemove: (value: any) => void;
    format?: (item: SelectListItem) => string;
}

class Selected extends React.PureComponent<Props> {
    handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        this.props.onRemove(this.props.item.value);
    }

    render() {
        return (
            <div className={styles.selected}>
                <span>{this.props.format ? this.props.format(this.props.item) : this.props.item.text}</span>
                <button type="button" onClick={this.handleClick}>×</button>
            </div>
        );
    }
}

export default Selected;