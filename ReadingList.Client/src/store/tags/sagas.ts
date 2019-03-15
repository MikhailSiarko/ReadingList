import { TagActionType } from './actionTypes';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from '../saga';
import { takeLeading, put } from 'redux-saga/effects';
import { Action } from 'redux';
import { TagsService } from '../../services';
import { Tag } from '../../models';
import { tagActions } from './actions';

function* fetchTagsAsync(action: Action) {
    if(isActionOf(tagActions.fetchTagBegin, action)) {
        yield executeAsync(
            () => new TagsService().getTags(),
            function* (tags: Tag[]) {
                yield put(tagActions.fetchTagSuccess(tags));
            }
        );
    }
}

export function* watchTagsSaga() {
    yield takeLeading(TagActionType.FETCH_TAGS_BEGIN, fetchTagsAsync);
}