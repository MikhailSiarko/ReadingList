import * as React from 'react';
import { FixedGroup, RoundButton } from 'src/controls';

interface Props {
    onShare: () => void;
    onAddBook: () => void;
}

const PrivateListActions: React.SFC<Props> = props => (
    <FixedGroup>
        <RoundButton radius={3} title="Share this list" onClick={props.onShare}>
            <i className="fas fa-share-alt" />
        </RoundButton>
        <RoundButton radius={3} title="Add book" onClick={props.onAddBook}>
            <i className="fas fa-book" />
        </RoundButton>
    </FixedGroup>
);

export default PrivateListActions;