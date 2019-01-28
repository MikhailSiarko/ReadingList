import * as React from 'react';
import styles from './FixedGroup.css';

interface Props extends React.HTMLProps<HTMLDivElement> {
    children: (JSX.Element | null)[] | (JSX.Element | null);
}

const FixedGroup: React.SFC<Props> = props => (
    <div {...props} className={styles['fixed-group']}>
        {
            Array.isArray(props.children)
            ? (
                (props.children as Array<JSX.Element>).map((element, index) => {
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