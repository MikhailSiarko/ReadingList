import * as React from 'react';
import styles from './PrivateListLegend.css';

interface Props {
    name: string;
}

const PrivateListLegend: React.SFC<Props> = props => (
    <h3 className={styles.header}>
        {props.name}
    </h3>
);

export default PrivateListLegend;