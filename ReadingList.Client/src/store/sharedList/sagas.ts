import { SharedListService } from 'src/services';
import { executeAsync, execute } from '../saga';
import { put, takeLeading, all } from 'redux-saga/effects';
import { isActionOf } from 'typesafe-actions';
import { SharedListActionType } from './actionTypes';
import { watchSharedListItem } from './item/sagas';
import { Action } from 'redux';
import { sharedListActions } from './actions';
import { push } from 'connected-react-router';
import { watchModeratorsSaga } from './moderators/sagas';
import { moderatorActions } from './moderators/actions';
import { SharedBookList, Chunked, SharedBookListPreview } from 'src/models';
import { tagActions } from '../tags';

function* fetchListAsync(action: Action) {
    if(isActionOf(sharedListActions.fetchListBegin, action)) {
        yield executeAsync(
            () => new SharedListService().getList(action.payload),
            function* (list: SharedBookList) {
                yield put(sharedListActions.fetchListSuccess(list));
            },
            true
        );
    }
}

function* createListAsync(action: Action) {
    if(isActionOf(sharedListActions.createListBegin, action)) {
        yield executeAsync(
            () => new SharedListService().createList(action.payload),
            null,
            true
        );
    }
}

function* fetchListsAsync(action: Action) {
    if(isActionOf(sharedListActions.fetchListsBegin, action)) {
        yield executeAsync(
            () => new SharedListService().getLists(action.payload.query, action.payload.chunk, null),
            function* (lists: Chunked<SharedBookListPreview>) {
                yield put(sharedListActions.fetchListsSuccess(lists));
            },
            true
        );
    }
}

function* updateListAsync(action: Action) {
    if(isActionOf(sharedListActions.updateListBegin, action)) {
        yield executeAsync(
            () => new SharedListService().updateList(action.payload.listId, action.payload.data),
            function* (list: SharedBookList) {
                yield put(sharedListActions.updateListSuccess(list));
            },
            true
        );
    }
}

function* switchListAsync(action: Action) {
    if(isActionOf(sharedListActions.switchListEditModeBegin, action)) {
        yield execute(
            function* () {
                yield put(moderatorActions.fetchModeratorsBegin());
                yield put(tagActions.fetchTagBegin());
                yield put(sharedListActions.switchListEditModeSuccess());
            },
            true
        );
    }
}

function* deleteListAsync(action: Action) {
    if(isActionOf(sharedListActions.deleteListBegin, action)) {
        yield executeAsync(
            () => new SharedListService().deleteList(action.payload),
            function* () {
                yield put(sharedListActions.deleteListSuccess());
                yield put(push('/shared/search'));
            },
            true
        );
    }
}

function* watchSharedList() {
    yield takeLeading(SharedListActionType.FETCH_SHARED_LIST_BEGIN, fetchListAsync);
    yield takeLeading(SharedListActionType.FETCH_SHARED_LISTS_BEGIN, fetchListsAsync);
    yield takeLeading(SharedListActionType.UPDATE_SHARED_LIST_BEGIN, updateListAsync);
    yield takeLeading(SharedListActionType.SWITCH_SHARED_LIST_TO_EDIT_MODE_BEGIN, switchListAsync);
    yield takeLeading(SharedListActionType.DELETE_SHARED_LIST_BEGIN, deleteListAsync);
    yield takeLeading(SharedListActionType.CREATE_SHARED_LIST_BEGIN, createListAsync);
}

export function* rootSharedListSaga() {
    yield all([
        watchSharedListItem(),
        watchSharedList(),
        watchModeratorsSaga()
    ]);
}