import * as React from 'react';
import styles from '../styles/global.scss';

interface MenuItem {
    onClick: () => void;
    text: string;
}

export function closeContextMenues() {
    let menues = document.getElementsByClassName('context-menu');
    Array.from(menues).forEach(item => item.remove());
}

export function withContextMenu<P extends React.HTMLProps<HTMLElement>>(
    menuItems: MenuItem[],
    Child: React.ComponentType<P>) {

        function createMenu(items: MenuItem[]) {
            const contextMenu = document.createElement('div');
            contextMenu.className = 'context-menu ' + styles['context-menu'];

            items.map(item => {
                const button = document.createElement('button');
                button.onclick = item.onClick;
                button.innerText = item.text;
                contextMenu.appendChild(button);
            });
            return contextMenu;
        }

        function setMenuPosition(eventClientX: number, eventClientY: number, contextMenu: HTMLElement) {
            const screenW = window.innerWidth;
            const screenH = window.innerHeight;

            let rootW: number = 0;
            let rootH: number = 0;

            if (contextMenu) {
                rootW = contextMenu.offsetWidth;
                rootH = contextMenu.offsetHeight;
            }

            const right = (screenW - eventClientX) > rootW;
            const left = !right;
            const top = (screenH - eventClientY) > rootH;
            const bottom = !top;

            if (right && contextMenu) {
                contextMenu.style.left = `${eventClientX + 5}px`;
            }

            if (left && contextMenu) {
                contextMenu.style.left = `${eventClientX - rootW - 5}px`;
            }

            if (top && contextMenu) {
                contextMenu.style.top = `${eventClientY + 5}px`;
            }

            if (bottom && contextMenu) {
                contextMenu.style.top = `${eventClientY - rootH - 5}px`;
            }
        }

        return class extends React.Component<P> {
            static displayName = `withContextMenu(${Child.displayName || Child.name})`;
            private menuWrapper: HTMLDivElement;

            handleContextMenu = (event: React.MouseEvent<HTMLElement>) => {
                event.persist();
                event.preventDefault();
                event.stopPropagation();

                closeContextMenues();

                let contextMenu = createMenu(menuItems);

                this.menuWrapper.appendChild(contextMenu);

                setMenuPosition(event.clientX, event.clientY, contextMenu);
            }

            handleWindowClick = (event: MouseEvent) => {
                const target = event.target as Node;
                if (this.menuWrapper) {
                    const contextMenu = this.menuWrapper.firstElementChild;
                    if (contextMenu) {
                        const wasOutside = target.contains(contextMenu);
                        if (wasOutside) {
                            contextMenu.remove();
                        }
                    }
                }
            }

            hideContextMenu = () => {
                if (this.menuWrapper) {
                    const contextMenu = this.menuWrapper.firstElementChild;
                    if (contextMenu) {
                        contextMenu.remove();
                    }
                }
            }

            componentDidMount() {
                document.addEventListener('click', this.handleWindowClick);
                document.addEventListener('scroll', this.hideContextMenu);
            }

            componentWillUnmount() {
                document.removeEventListener('click', this.handleWindowClick);
                document.removeEventListener('scroll', this.hideContextMenu);
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
                                    this.hideContextMenu();
                                }
                            }
                        />
                        <div
                            className={styles['context-menu-wrapper']}
                            ref={ref => this.menuWrapper = ref as HTMLDivElement}
                        />
                    </div>
                );
            }
        };
}
