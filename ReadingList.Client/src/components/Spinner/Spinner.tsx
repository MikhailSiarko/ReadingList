import * as React from 'react';
import style from './Spinner.css';

interface Props {
    loading?: boolean;
}

const Spinner: React.SFC<Props> = (props) => {
    return (
        <div className={style['spinner-wrapper']} hidden={!props.loading}>
            <div className={style.spinner} />
        </div>
    );
};

export default Spinner;