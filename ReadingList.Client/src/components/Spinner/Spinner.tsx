import * as React from 'react';
import style from './Spinner.css';

interface Props {
}

const Spinner: React.SFC<Props> = props => (
    <div className={style['spinner-wrapper']}>
        <div className={style.spinner} />
    </div>
);

export default Spinner;