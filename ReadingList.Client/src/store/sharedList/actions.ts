import { ActionType, createAction } from 'typesafe-actions';
import { SharedListActionType } from './actionTypes';
import {
    SharedBookList,
    SharedListCreateData,
    SharedListUpdateData,
    Chunked,
    SharedBookListPreview
} from '../../models';
import { merge } from 'lodash';
import { sharedItemActions } from './item/actions';
import { moderatorActions } from './moderators/actions';

const actions = {
    createListBegin: createAction(
        SharedListActionType.CREATE_SHARED_LIST_BEGIN,
        action => (data: SharedListCreateData) => action(data)
    ),
    updateListBegin: createAction(
        SharedListActionType.UPDATE_SHARED_LIST_BEGIN,
        action => (listId: number, data: SharedListUpdateData) => action({ listId, data })
    ),
    updateListSuccess: createAction(
        SharedListActionType.UPDATE_SHARED_LIST_SUCCESS,
        action => (list: SharedBookList) => action(list)
    ),
    fetchListBegin: createAction(
        SharedListActionType.FETCH_SHARED_LIST_BEGIN,
        action => (id: number) => action(id)
    ),
    fetchListSuccess: createAction(
        SharedListActionType.FETCH_SHARED_LIST_SUCCESS,
        action => (list: SharedBookList) => action(list)
    ),
    fetchListsBegin: createAction(
        SharedListActionType.FETCH_SHARED_LISTS_BEGIN,
        action => (query: string, chunk: number | null) => action({ query, chunk })
    ),
    fetchListsSuccess: createAction(
        SharedListActionType.FETCH_SHARED_LISTS_SUCCESS,
        action => (lists: Chunked<SharedBookListPreview>) => action(lists)
    ),
    clearShared: createAction(SharedListActionType.CLEAR_SHARED_LIST_STATE),
    switchListEditModeBegin: createAction(SharedListActionType.SWITCH_SHARED_LIST_TO_EDIT_MODE_BEGIN),
    switchListEditModeSuccess: createAction(SharedListActionType.SWITCH_SHARED_LIST_TO_EDIT_MODE_SUCCESS),
    switchListSimpleMode: createAction(SharedListActionType.SWITCH_SHARED_LIST_TO_SIMPLE_MODE),
    deleteListBegin: createAction(
        SharedListActionType.DELETE_SHARED_LIST_BEGIN,
        action => (id: number) => action(id)
    ),
    deleteListSuccess: createAction(SharedListActionType.DELETE_SHARED_LIST_SUCCESS)
};

export const sharedListActions = merge(actions, sharedItemActions, moderatorActions);
export type SharedListAction = ActionType<typeof sharedListActions>;