import * as React from 'react';
import Layout from '../Layout';
import styles from './Main.css';
import { HTMLProps } from 'react';

const Main = (props: HTMLProps<any>) => {
    return (
        <Layout element={'main'} className={styles.main}>
            {props.children}
        </Layout>
    );
};

export default Main;