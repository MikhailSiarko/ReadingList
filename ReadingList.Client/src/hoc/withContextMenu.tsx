import * as React from 'react';
import styles from '../styles/global.css';

interface MenuItem {
    onClick: () => void;
    text: string;
}

interface ContextMenuState {
    isVisible: boolean;
}
// TODO: Fix handleContextMenu on another contexed item
export function withContextMenu<P>(menuItems: MenuItem[], Child: React.ComponentType<P>) {
    return class extends React.Component<P, ContextMenuState> {
        static displayName = `withContextMenu(${Child.displayName || Child.name})`;
        private contextMenu: HTMLDivElement;
        private wrapper: HTMLDivElement;
        constructor(props: P) {
            super(props);
            this.state = { isVisible: false };
        }

        handleContextMenu = (event: MouseEvent) => {
            event.preventDefault();
            event.stopPropagation();
            this.setState({isVisible: true}, () => {
                const clickX = event.clientX;
                const clickY = event.clientY;
                const screenW = window.innerWidth;
                const screenH = window.innerHeight;

                let rootW: number = 0;
                let rootH: number = 0;

                if(this.contextMenu) {
                    rootW = this.contextMenu.offsetWidth;
                    rootH = this.contextMenu.offsetHeight;
                }

                const right = (screenW - clickX) > rootW;
                const left = !right;
                const top = (screenH - clickY) > rootH;
                const bottom = !top;

                if (right && this.contextMenu) {
                    this.contextMenu.style.left = `${clickX + 5}px`;
                }

                if (left && this.contextMenu) {
                    this.contextMenu.style.left = `${clickX - rootW - 5}px`;
                }

                if (top && this.contextMenu) {
                    this.contextMenu.style.top = `${clickY + 5}px`;
                }

                if (bottom && this.contextMenu) {
                    this.contextMenu.style.top = `${clickY - rootH - 5}px`;
                }
            });
        }

        handleWindowClick = (event: MouseEvent) => {
            const { isVisible } = this.state;
            const target = event.target as Node;
            const wasOutside = target.contains(this.contextMenu);

            if (wasOutside && isVisible) {
                this.setState({ isVisible: false, });
            }
        }

        handleClick = () => {
            this.setState({ isVisible: false });
        }

        handleScroll = () => {
            this.setState({ isVisible: false, });
        }

        componentDidMount() {
            if(this.wrapper) {
                this.wrapper.addEventListener('contextmenu', this.handleContextMenu);
            }
            document.addEventListener('click', this.handleClick);
            document.addEventListener('scroll', this.handleScroll);
        }

        componentWillUnmount() {
            if(this.wrapper) {
                this.wrapper.removeEventListener('contextmenu', this.handleContextMenu);
            }
            document.removeEventListener('click', this.handleClick);
            document.removeEventListener('scroll', this.handleScroll);
        }

        render() {
            return (
                <div onClick={this.handleClick} ref={ref => this.wrapper = ref as HTMLDivElement}>
                    <Child {...this.props} />
                    <div className={styles['context-menu-wrapper']}>
                        {
                            this.state.isVisible
                                ?
                                    <div ref={(ref) => this.contextMenu = ref as HTMLDivElement}
                                        className={styles['context-menu']}>
                                        {
                                            menuItems.map((item) => (
                                                    <button key={item.text} onClick={item.onClick}>
                                                        {item.text}
                                                    </button>
                                                ))
                                        }
                                    </div>
                                :
                                    null
                        }
                    </div>
                </div>
            );
        }
    };
}
