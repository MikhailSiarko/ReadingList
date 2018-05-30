import * as React from 'react';
import styles from './ContextMenu.css';

export interface ContextMenuProps {
    menuItems: {
        onClick: () => void;
        text: string;
    }[];
    rootId: string;
}

export interface ContextMenuState {
    isVisible: boolean;
}

class ContextMenu extends React.Component<ContextMenuProps, ContextMenuState> {
    private contextMenu: HTMLDivElement;
    constructor(props: ContextMenuProps) {
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

    handleClick = (event: MouseEvent) => {
        this.setState({ isVisible: false });
    }

    handleScroll = (event: MouseEvent) => {
        this.setState({ isVisible: false, });
    }

    componentDidMount() {
        const rootElement = document.getElementById(this.props.rootId);
        if(rootElement) {
            rootElement.addEventListener('contextmenu', this.handleContextMenu);
        } 
        document.addEventListener('click', this.handleClick);
        document.addEventListener('scroll', this.handleScroll);
    }

    componentWillUnmount() {
        const rootElement = document.getElementById(this.props.rootId);
        if(rootElement) {
            rootElement.removeEventListener('contextmenu', this.handleContextMenu);
        } 
        document.removeEventListener('click', this.handleClick);
        document.removeEventListener('scroll', this.handleScroll);
    }

    render() {
        return (
            <div className={styles['context-menu-wrapper']} 
                    onClick={(event: React.MouseEvent<HTMLElement>) => this.handleClick(event.nativeEvent)}>
                {
                    this.state.isVisible
                        ?
                            <div ref={(ref) => this.contextMenu = ref as HTMLDivElement}
                                 className={styles['context-menu']}>
                                {
                                    this.props.menuItems.map((item) => (
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
        );
    }
}

export default ContextMenu;