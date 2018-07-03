import * as React from 'react';
import { NavLink } from 'react-router-dom';

export type NavBarLinkData = {
    text: string;
    href: string;
    action?: (event: React.MouseEvent<HTMLAnchorElement>) => void;
};

interface NavBarLinkProps {
    link: NavBarLinkData;
    className: string;
    activeClassName?: string;
}

const renderAnchor = (props: NavBarLinkProps) => (
    <a className={props.className} onClick={props.link.action}>{props.link.text}</a>
);

const renderNavLink = (props: NavBarLinkProps) => (
    <NavLink 
        className={props.className}
        activeClassName={props.activeClassName}
        to={props.link.href}
    >
        {props.link.text}
    </NavLink>
);

const NavBarLink: React.SFC<NavBarLinkProps> = props => props.link.action ? renderAnchor(props) : renderNavLink(props);

export default NavBarLink;