import * as React from 'react';
import styles from './Container.scss';
import * as classNames from 'classnames';

interface Props {
    width: number;
    unit: string;
    className?: string;
}

const Container: React.SFC<Props> = props => (
    <div
        style={
            {
                width: props.width + props.unit
            }
        }
        className={classNames(styles.container, props.className)}
    >
        {props.children}
    </div>
);

export default Container;