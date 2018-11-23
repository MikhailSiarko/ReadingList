import * as React from 'react';
import * as ReactDOM from 'react-dom';
import registerServiceWorker from './registerServiceWorker';
import configureStore, { history } from './store';
import { Provider } from 'react-redux';
import App from './containers/App';
import { ConnectedRouter } from 'connected-react-router';
import './styles/global.css';

let store = configureStore();

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App />
        </ConnectedRouter>
    </Provider>,
    document.getElementById('root') as HTMLElement
);
registerServiceWorker();