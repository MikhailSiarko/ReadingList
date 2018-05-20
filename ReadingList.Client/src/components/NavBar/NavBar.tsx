import * as React from 'react';
import NavBarLink, { NavBarLinkData } from './NavBarLink';
import Layout from '../Layout';
import styles from './NavBar.css';

interface NavBarProps {
    links: NavBarLinkData[];
}

class NavBar extends React.Component<NavBarProps> {
    render() {
        const navLinks = this.props.links.map((value, index) => {
            return (
                <NavBarLink className={styles['nav-bar-link']} activeClassName={styles['active-nav-bar-link']}
                               link={value} key={'nav-link-' + index} />
            );
        });
        return (
            <Layout className={styles['nav-bar']} element={'nav'}>
                {navLinks}
            </Layout>
        );
    }
}

export default NavBar;