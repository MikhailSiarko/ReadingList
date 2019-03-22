import * as React from 'react';
import styles from '../styles/global.scss';
import classNames from 'classnames';
import * as ReactDOM from 'react-dom';

interface MenuItem {
    onClick: () => void;
    text: string;
}

type Position = { top: string, left: string };

export function closeContextMenues() {
    const wrappers = document.getElementsByClassName(styles['context-menu-wrapper']);
    Array.from(wrappers).forEach(w => ReactDOM.unmountComponentAtNode(w));
}

export function withContextMenu<P extends React.HTMLProps<HTMLElement>>(
    menuItems: MenuItem[],
    Child: React.ComponentType<P>) {

        return class extends React.Component<P> {
            static displayName = `withContextMenu(${Child.displayName || Child.name})`;
            menuWrapper: HTMLDivElement;

            createMenu = (position: Position) => {
                return (
                    <div
                        className={classNames('context-menu', styles['context-menu'])}
                        style={{
                            top: position.top,
                            left: position.left,
                        }}
                    >
                        {
                            menuItems.map((item, index) => {
                                return (
                                    <button key={index} onClick={item.onClick}>
                                        {item.text}
                                    </button>
                                );
                            })
                        }
                    </div>
                );
            }

            getPosition(eventClientX: number, eventClientY: number, contextMenu: Element | null): Position {
                const screenW = window.innerWidth;
                const screenH = window.innerHeight;

                let rootW: number = 0;
                let rootH: number = 0;

                const menu = contextMenu as HTMLElement;

                if (menu) {
                    rootW = menu.offsetWidth;
                    rootH = menu.offsetHeight;
                }

                const right = (screenW - eventClientX) > rootW;
                const left = !right;
                const top = (screenH - eventClientY) > rootH;
                const bottom = !top;

                let position: Position = {
                    left: '0',
                    top: '0'
                };

                if (right && menu) {
                    position.left = `${eventClientX + 5}px`;
                }

                if (left && menu) {
                    position.left = `${eventClientX}px`;
                }

                if (top && menu) {
                    position.top = `${eventClientY + 5}px`;
                }

                if (bottom && menu) {
                    position.top = `${eventClientY}px`;
                }

                return position;
            }

            handleContextMenu = (event: React.MouseEvent<HTMLElement>) => {
                event.persist();
                event.preventDefault();
                event.stopPropagation();
                event.nativeEvent.stopImmediatePropagation();
                closeContextMenues();
                const position = this.getPosition(event.clientX, event.clientY, this.menuWrapper);
                const menu = this.createMenu(position);
                ReactDOM.render(menu, this.menuWrapper);
            }

            handleWindowClick = (event: MouseEvent) => {
                const target = event.target as Node;
                if (this.menuWrapper) {
                    const menu = this.menuWrapper.firstElementChild;
                    if(menu) {
                        const wasOutside = target.contains(menu);
                        if (wasOutside) {
                            ReactDOM.unmountComponentAtNode(this.menuWrapper);
                        }
                    }
                }
            }

            hideContextMenues = () => {
                closeContextMenues();
            }

            componentDidMount() {
                document.addEventListener('click', this.handleWindowClick);
                document.addEventListener('scroll', this.hideContextMenues);
            }

            componentWillUnmount() {
                document.removeEventListener('click', this.handleWindowClick);
                document.removeEventListener('scroll', this.hideContextMenues);
            }

            getMenuWrapperRef = (ref: HTMLDivElement) => {
                this.menuWrapper = ref;
            }

            render() {
                return (
                    <div>
                        <Child
                            {...this.props}
                            onContextMenu={this.handleContextMenu}
                            onClick={
                                (event) => {
                                    if(this.props.onClick) {
                                        this.props.onClick(event);
                                    }
                                    this.hideContextMenues();
                                }
                            }
                        />
                        <div className={styles['context-menu-wrapper']} ref={this.getMenuWrapperRef} />
                    </div>
                );
            }
        };
}
