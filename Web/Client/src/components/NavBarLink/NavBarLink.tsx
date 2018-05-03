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

const NavBarLink = (props: NavBarLinkProps) => {
    const link = props.link.action
        ? (<a className={props.className} onClick={props.link.action}>{props.link.text}</a>)
        : (
            <NavLink onClick={props.link.action} className={props.className} activeClassName={props.activeClassName}
                        to={props.link.href}>
                {props.link.text}
            </NavLink>
        );
    return link;
};

export default NavBarLink;