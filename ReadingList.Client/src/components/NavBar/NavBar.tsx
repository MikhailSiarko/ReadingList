import * as React from 'react';
import NavBarLink, { NavBarLinkData } from './NavBarLink';
import styles from './NavBar.css';
import { cloneDeep } from 'lodash';

interface NavBarProps extends React.HTMLProps<HTMLElement> {
    links: NavBarLinkData[];
}

const NavBar: React.SFC<NavBarProps> = props => {
    const navLinks = props.links.map((value, index) => (
        <NavBarLink 
            className={styles['nav-bar-link']} 
            activeClassName={styles['active-nav-bar-link']}
            link={value}
            key={'nav-link-' + index} 
        />
    ));
    const propsCopy = cloneDeep(props);
    delete propsCopy.links;
    return (
        <nav className={styles['nav-bar']} {...propsCopy}>
            {navLinks}
        </nav>
    );
};

export default NavBar;