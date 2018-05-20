import * as React from 'react';
import styles from '../BookUL/BookLI/BookLI.css';

interface ContextMenuProps {
    element: string;
    menuItems: {
        onClick: () => void;
        text: string;
    }[];
    className?: string;
}

interface ContextMenuState {
    isVisible: boolean;
}

class ContextMenu extends React.Component<ContextMenuProps, ContextMenuState> {
    private root: HTMLElement;
    private contextMenu: HTMLDivElement;
    constructor(props: ContextMenuProps) {
        super(props);
        this.state = { isVisible: false };
    }

    handleContextMenu = (event: MouseEvent) => {
        event.preventDefault();
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

            if (left && this.root) {
                this.contextMenu.style.left = `${clickX - rootW - 5}px`;
            }

            if (top && this.root) {
                this.contextMenu.style.top = `${clickY + 5}px`;
            }

            if (bottom && this.root) {
                this.contextMenu.style.top = `${clickY - rootH - 5}px`;
            }
        });
    }

    handleClick = (event: MouseEvent) => {
        const { isVisible } = this.state;
        const target = event.target as Node;
        const wasOutside = !(target.contains(this.contextMenu));

        if (wasOutside && isVisible) {
            this.setState({ isVisible: false, });
        }
    }

    handleScroll = (event: MouseEvent) => {
        const { isVisible } = this.state;
        const wasOutside = !(event.target === this.contextMenu);

        if (wasOutside && isVisible) {
            this.setState({ isVisible: false, });
        }
    }

    componentDidMount() {
        this.root.addEventListener('contextmenu', this.handleContextMenu);
        document.addEventListener('click', this.handleClick);
        document.addEventListener('scroll', this.handleScroll);
    }

    componentWillUnmount() {
        this.root.removeEventListener('contextmenu', this.handleContextMenu);
        document.removeEventListener('click', this.handleClick);
        document.removeEventListener('scroll', this.handleScroll);
    }

    render() {
        return (
            <this.props.element ref={(ref: any) => this.root = ref as HTMLElement} className={this.props.className}>
                {this.props.children}
                {
                    this.state.isVisible
                        ?
                            <div ref={(ref) => this.contextMenu = ref as HTMLDivElement}
                                 className={styles['context-menu']}>
                                {
                                    this.props.menuItems.map((item) => (
                                        <button key={item.text} className={styles['menu-option']}
                                                onClick={() => item.onClick()}>
                                            {item.text}
                                        </button>
                                    ))
                                }
                            </div>
                        :
                            null
                }

            </this.props.element>
        );
    }
}

export default ContextMenu;