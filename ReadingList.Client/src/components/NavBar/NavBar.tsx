import * as React from 'react';
import NavBarLink, { NavBarLinkData } from './NavBarLink';
import Layout from '../Layout';

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
            <Layout className="nav-bar" element={'nav'}>
                {navLinks}
            </Layout>
        );
    }
}

export default NavBar;