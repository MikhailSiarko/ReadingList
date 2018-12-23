import * as React from 'react';
import Container from '../Container';
import globalStyles from '../../styles/global.css';
import MultiSelect from '../MultiSelect';
import { SelectListItem } from '../../models';

interface Props {
    tags: SelectListItem[];
}

const CreateSharedList: React.SFC<Props> = props => (
    <Container width={80} unit={'%'}>
        <div>
            <input
                type="text"
                name="name"
                required={true}
                placeholder="Enter the name"
                className={globalStyles.shadowed}
            />
        </div>
        <div>
            <MultiSelect
                name="tags"
                placeholder={'Select tags'}
                options={props.tags}
                selectedFormat={item => `#${item.text}`}
                addNewIfNotFound={true}
            />
        </div>
    </Container>
);

export default CreateSharedList;