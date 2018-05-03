import * as React from 'react';
import NavBarLink, { NavBarLinkData } from '../NavBarLink';

interface NavBarProps {
    links: NavBarLinkData[];
}

class NavBar extends React.Component<NavBarProps> {
    render() {
        const navLinks = this.props.links.map((value, index) => {
            return (
                <NavBarLink className={'nav-bar-link'} activeClassName={'active-nav-bar-link'}
                               link={value} key={'nav-link-' + index} />
            );
        });
        return (
            <nav className="nav-bar">
                {navLinks}
            </nav>
        );
    }
}

export default NavBar;