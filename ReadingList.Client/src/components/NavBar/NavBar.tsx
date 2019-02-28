import * as React from 'react';
import NavBarLink, { NavBarLinkData } from './NavBarLink';
import styles from './NavBar.scss';
import { createDOMAttributeProps } from '../../utils';

interface NavBarProps extends React.HTMLProps<HTMLElement> {
    links: NavBarLinkData[];
}

const NavBar: React.SFC<NavBarProps> = props => {
    const navLinks = props.links.map((value, index) => <NavBarLink link={value} key={'nav-link-' + index} />);
    const propsWithoutLinks = createDOMAttributeProps(props, 'links');
    return (
        <nav className={styles['nav-bar']} {...propsWithoutLinks}>
            {navLinks}
        </nav>
    );
};

export default NavBar;