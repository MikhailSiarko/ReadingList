import { TagActionType } from './actionTypes';
import { isActionOf } from 'typesafe-actions';
import { executeAsync } from 'src/store/saga';
import { takeLeading, put } from 'redux-saga/effects';
import { Action } from 'redux';
import { TagsService } from 'src/services';
import { Tag } from 'src/models';
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