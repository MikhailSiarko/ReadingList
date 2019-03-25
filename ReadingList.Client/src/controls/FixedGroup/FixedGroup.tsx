import * as React from 'react';
import styles from './FixedGroup.scss';

interface Props extends React.HTMLProps<HTMLDivElement> {
    children: React.ReactNode;
}

const FixedGroup: React.SFC<Props> = props => (
    <div {...props} className={styles['fixed-group']}>
        {
            Array.isArray(props.children)
            ? (
                React.Children.map(props.children, (element, index) => {
                    return (
                        <div key={index} className={styles['element-wrapper']}>
                            {element}
                        </div>
                    );
                })
            )
            : (
                <div className={styles['element-wrapper']}>
                    {props.children}
                </div>
            )
        }
    </div>
);

export default FixedGroup;