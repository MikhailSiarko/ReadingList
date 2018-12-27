import * as React from 'react';
import styles from './FixedGroup.css';

interface Props extends React.HTMLProps<HTMLDivElement> {
    children: JSX.Element[];
}

const FixedGroup: React.SFC<Props> = props => (
    <div {...props} className={styles['fixed-group']}>
        {
            props.children.map((element, index) => {
                return (
                    <div key={index} className={styles['element-wrapper']}>
                        {element}
                    </div>
                );
            })
        }
    </div>
);

export default FixedGroup;