import * as React from 'react';
import styles from './Main.css';

const Main = (props: React.HTMLProps<any>) => {
    return (
        <main className={styles.main}>
            {props.children}
        </main>
    );
};

export default Main;