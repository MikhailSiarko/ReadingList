import * as React from 'react';
import Layout from '../Layout';
import styles from './Main.css';

const Main = (props: React.Props<any>) => {
    return (
        <Layout element={'main'} className={styles.main}>
            {props.children}
        </Layout>
    );
};

export default Main;