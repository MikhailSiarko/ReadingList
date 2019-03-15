export enum SharedListActionType {
    CREATE_SHARED_LIST_BEGIN = 'CREATE_SHARED_LIST_BEGIN',
    FETCH_SHARED_LIST_BEGIN = 'FETCH_SHARED_LIST_BEGIN',
    FETCH_SHARED_LIST_SUCCESS = 'FETCH_SHARED_LIST_SUCCESS',
    FETCH_SHARED_LISTS_BEGIN = 'FETCH_SHARED_LISTS_BEGIN',
    FETCH_SHARED_LISTS_SUCCESS = 'FETCH_SHARED_LISTS_SUCCESS',
    UPDATE_SHARED_LIST_BEGIN = 'UPDATE_SHARED_LIST_BEGIN',
    UPDATE_SHARED_LIST_SUCCESS = 'UPDATE_SHARED_LIST_SUCCESS',
    CLEAR_SHARED_LIST_STATE = 'CLEAR_SHARED_LIST_STATE',
    SWITCH_SHARED_LIST_TO_EDIT_MODE_BEGIN = 'SWITCH_SHARED_LIST_TO_EDIT_MODE_BEGIN',
    SWITCH_SHARED_LIST_TO_EDIT_MODE_SUCCESS = 'SWITCH_SHARED_LIST_TO_EDIT_MODE_SUCCESS',
    SWITCH_SHARED_LIST_TO_SIMPLE_MODE = 'SWITCH_SHARED_LIST_TO_SIMPLE_MODE',
    DELETE_SHARED_LIST_BEGIN = 'DELETE_SHARED_LIST_BEGIN',
    DELETE_SHARED_LIST_SUCCESS = 'DELETE_SHARED_LIST_SUCCESS'
}