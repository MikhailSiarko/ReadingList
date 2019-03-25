import * as React from 'react';
import { NavBarLink, NavBarLinkData } from './NavBarLink';
import styles from './NavBar.scss';

interface NavBarProps extends React.HTMLProps<HTMLElement> {
    links: NavBarLinkData[];
}

const NavBar: React.SFC<NavBarProps> = props => {
    const { links, ...restOfProps } = props;
    const navLinks = links.map((value, index) => <NavBarLink link={value} key={'nav-link-' + index} />);
    return (
        <nav className={styles['nav-bar']} {...restOfProps}>
            {navLinks}
        </nav>
    );
};

export default NavBar;