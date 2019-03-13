import { PrivateListItemStatusActionType } from './actionTypes';
import { createAction } from 'typesafe-actions';
import { SelectListItem } from 'src/models';

export const itemStatusActions = {
    fetchItemStatusesBegin: createAction(PrivateListItemStatusActionType.FETCH_PRIVATE_ITEM_STATUSES_BEGIN),
    fetchItemStatusesSuccess: createAction(
        PrivateListItemStatusActionType.FETCH_PRIVATE_ITEM_STATUSES_SUCCESS,
        action => (statuses: SelectListItem[]) => action(statuses)
    )
};