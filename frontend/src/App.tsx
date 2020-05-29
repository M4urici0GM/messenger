import React from 'react';
import { Provider } from 'react-redux';
import { ToastContainer } from 'react-toastify';

import store from './store';

import Router from './router';
import AppContainer from './components/AppContainer';

import './index.css';
import 'react-toastify/dist/ReactToastify.css'

function App() {
  return (
    <Provider store={store}>
      <AppContainer>
        <ToastContainer />
        <Router />
      </AppContainer>
    </Provider>
  );
}

export default App;
