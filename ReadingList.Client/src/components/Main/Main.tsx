import * as React from 'react';
import styles from './Main.css';

const Main: React.SFC<React.HTMLProps<HTMLMainElement>> = props => (
    <main className={styles.main} {...props}>
        {props.children}
    </main>
);

export default Main;