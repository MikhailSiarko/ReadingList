import { applyMiddleware, compose, createStore, GenericStoreEnhancer } from 'redux';
import createBrowserHistory from 'history/createBrowserHistory';
import { connectRouter, routerMiddleware } from 'connected-react-router';
import { rootReducer, RootState } from './state';
import { SagaMiddleware } from 'redux-saga';

export * from './authentication';
export * from './initialState';
export * from './loading';
export * from './notification';
export * from './privateList';
export * from './books';
export * from './moderatedList';
export * from './saga';
export * from './state';
export * from './tags';

declare global {
  interface Window {
    __REDUX_DEVTOOLS_EXTENSION__: () => undefined;
    __REDUX_DEVTOOLS_EXTENSION_COMPOSE__: (arg: GenericStoreEnhancer) => undefined;
  }
}

export const history = createBrowserHistory();
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const getHistory = () => history;

export default function configureStore(sagaMiddleware: SagaMiddleware, initialState?: RootState) {
  return createStore(
    connectRouter(history)(rootReducer),
    initialState,
    composeEnhancers(applyMiddleware(routerMiddleware(history), sagaMiddleware))
  );
}