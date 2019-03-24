import * as React from 'react';
import styles from './Container.scss';
import * as classNames from 'classnames';

interface Props {
    className?: string;
}

const Container: React.SFC<Props> = props => (
    <div
        className={classNames(styles.container, props.className)}
    >
        {props.children}
    </div>
);

export default Container;