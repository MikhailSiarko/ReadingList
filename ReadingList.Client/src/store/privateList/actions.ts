import { ActionType, createAction } from 'typesafe-actions';
import { PrivateListActionType } from './actionTypes';
import { PrivateBookList } from 'src/models';
import { merge } from 'lodash';
import { itemActions } from './item';
import { itemStatusActions } from './itemStatus';

const listActions = {
    updateListBegin: createAction(
        PrivateListActionType.UPDATE_PRIVATE_LIST_BEGIN,
        action => (list: PrivateBookList) => action(list)
    ),
    updateListSuccess: createAction(
        PrivateListActionType.UPDATE_PRIVATE_LIST_SUCCESS,
        action => (list: PrivateBookList) => action(list)
    ),
    fetchListBegin: createAction(PrivateListActionType.FETCH_PRIVATE_LIST_BEGIN),
    fetchListSuccess: createAction(
        PrivateListActionType.FETCH_PRIVATE_LIST_SUCCESS,
        action => (list: PrivateBookList) => action(list)),
    unsetPrivate: createAction(PrivateListActionType.UNSET_PRIVATE_LIST),
    switchListEditMode: createAction(PrivateListActionType.SWITCH_PRIVATE_LIST_MODE),
    shareList: createAction(
        PrivateListActionType.SHARE_PRIVATE_LIST,
        action => (name: string) => action(name)
    )
};

export const privateListActions = merge(listActions, itemActions, itemStatusActions);

export type PrivateListAction = ActionType<typeof privateListActions>;