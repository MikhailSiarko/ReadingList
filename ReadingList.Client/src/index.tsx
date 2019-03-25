import * as React from 'react';
import * as ReactDOM from 'react-dom';
import registerServiceWorker from './registerServiceWorker';
import configureStore, { history, rootSaga } from './store';
import { Provider } from 'react-redux';
import App from './containers/App';
import { ConnectedRouter } from 'connected-react-router';
import './styles/global.scss';
import createSagaMiddleware from 'redux-saga';

const sagaMiddleware = createSagaMiddleware();

let store = configureStore(sagaMiddleware);

sagaMiddleware.run(rootSaga);

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App />
        </ConnectedRouter>
    </Provider>,
    document.getElementById('root') as HTMLElement
);
registerServiceWorker();