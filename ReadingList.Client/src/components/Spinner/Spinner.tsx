import * as React from 'react';
import { default as posed } from 'react-pose';
import style from './Spinner.scss';

interface Props {
    loading: boolean;
}

const Animated = posed.div({
    enter: {
        opacity: 1,
        zIndex: 999,
        delay: 200,
        transition: {
            opacity: { ease: 'easeOut', duration: 500 },
            zIndex: { ease: 'easeOut', duration: 600 }
        }
    },
    exit: {
        opacity: 0,
        zIndex: -2,
        delay: 200,
        transition: {
            opacity: { ease: 'easeOut', duration: 500 },
            zIndex: { ease: 'easeOut', duration: 600 }
        }
    }
});

const Spinner: React.SFC<Props> = props => {
    return (
        props.loading ? (
            <Animated
                className={style['spinner-wrapper']}
            >
                <div className={style.spinner} />
            </Animated>
        ) : null
    );
};

export default Spinner;