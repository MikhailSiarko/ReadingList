import * as React from 'react';
import { reduceTags } from '../../utils';
import { Moderator, Tag } from '../../models';
import styles from './SharedListLegend.scss';

interface Props {
    name: string;
    tags: Tag[];
    moderators: Moderator[];
}

const SharedListLegend: React.SFC<Props> = props => (
    <div className={styles['wrapper']}>
        <h4 className={styles['header']}>{props.name}</h4>
        <div className={styles['info']}>
            {
                props.tags.length > 0 &&
                    <div className={styles['additional-info']}>
                        tags: {reduceTags(props.tags.map(i => i.name))}
                    </div>
            }
            {
                props.moderators.length > 0 &&
                    <div className={styles['additional-info']}>
                        moderators: {props.moderators.map(m => m.login).join(' ')}
                    </div>
            }
        </div>
    </div>
);

export default SharedListLegend;