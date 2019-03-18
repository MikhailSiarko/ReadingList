import * as React from 'react';
import NavBarLink, { NavBarLinkData } from './NavBarLink';
import styles from './NavBar.scss';

interface NavBarProps extends React.HTMLProps<HTMLElement> {
    links: NavBarLinkData[];
}

const NavBar: React.SFC<NavBarProps> = props => {
    const navLinks = props.links.map((value, index) => <NavBarLink link={value} key={'nav-link-' + index} />);
    const { links, ...restOfProps } = props;
    return (
        <nav className={styles['nav-bar']} {...restOfProps}>
            {navLinks}
        </nav>
    );
};

export default NavBar;