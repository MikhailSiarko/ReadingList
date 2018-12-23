import * as React from 'react';
import styles from './Container.css';
import { applyClasses } from '../../utils';

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
        className={applyClasses(styles.container, props.className ? props.className : '')}
    >
        {props.children}
    </div>
);

export default Container;