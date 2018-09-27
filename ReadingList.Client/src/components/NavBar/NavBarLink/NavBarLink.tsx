import * as React from 'react';
import { NavLink } from 'react-router-dom';
import styles from './NavBarLink.css';

export type NavBarLinkData = {
    text: string;
    href: string;
    action?: (event: React.MouseEvent<HTMLAnchorElement>) => void;
};

interface NavBarLinkProps {
    link: NavBarLinkData;
}

const renderAnchor = (props: NavBarLinkProps) => (
    <a className={styles['nav-bar-link']} onClick={props.link.action}>{props.link.text}</a>
);

const renderNavLink = (props: NavBarLinkProps) => (
    <NavLink
        className={styles['nav-bar-link']}
        activeClassName={styles['active-nav-bar-link']}
        to={props.link.href}
    >
        {props.link.text}
    </NavLink>
);

const NavBarLink: React.SFC<NavBarLinkProps> = props => props.link.action ? renderAnchor(props) : renderNavLink(props);

export default NavBarLink;