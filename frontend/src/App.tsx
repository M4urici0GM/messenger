import React, { FC } from 'react';

import { ToastContainer } from 'react-toastify';
import { ThemeProvider } from 'styled-components';
import { connect } from 'react-redux';

import { GlobalState } from './store/index';
import { Theme } from './store/ducks/app/types';

import Router from './router';
import AppContainer from './components/AppContainer';

import './index.css';
import 'react-toastify/dist/ReactToastify.css'

interface StateProps {
  currentTheme: Theme
};

interface OwnProps {

};

type Props = StateProps & OwnProps;


const App: FC<Props> = (props) => (
  <ThemeProvider theme={props.currentTheme}>
    <AppContainer>
      <ToastContainer />
      <Router />
    </AppContainer>
  </ThemeProvider>
);


const mapStateToProps = (state: GlobalState) => ({
  currentTheme: state.app.themeMode,
})

export default connect(mapStateToProps)(App);
