import { RootState } from '../state';
import { initialState } from '../initialState';
import { getType } from 'typesafe-actions';
import { PrivateListAction, privateListActions } from './actions';
import { cloneDeep } from 'lodash';
import { PrivateBookList, List } from '../../models';

export function privateBookListReducer(state: RootState.Private = initialState.private, action: PrivateListAction) {
    const copy = cloneDeep(state);
    switch (action.type) {
        case getType(privateListActions.unsetPrivate):
            return initialState.private;
        case getType(privateListActions.switchListEditMode):
            if (copy.list) {
                List.switchMode(copy.list);
            }
            return copy;
        case getType(privateListActions.updateListSuccess):
        case getType(privateListActions.fetchListSuccess):
            copy.list = action.payload;
            return copy;
        case getType(privateListActions.addItemSuccess):
            if (copy.list) {
                List.add(copy.list, action.payload);
            }
            return copy;
        case getType(privateListActions.deleteItemSuccess):
            if (copy.list) {
                List.removeItem(copy.list, action.payload);
            }
            return copy;
        case getType(privateListActions.updateItemSuccess):
            if (copy.list) {
                List.updateItem(copy.list, action.payload);
            }
            return copy;
        case getType(privateListActions.switchItemEditMode):
            if (copy.list) {
                PrivateBookList.switchItemMode(copy.list, action.payload);
            }
            return copy;
        case getType(privateListActions.fetchItemStatusesSuccess):
            copy.bookStatuses = action.payload;
            return copy;
        default:
            return state;
    }
}