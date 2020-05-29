
import React from 'react';
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom';

import PrivateRoute from './components/PrivateRoute';

import Home from './pages/home';
import Signin from './pages/signin';

const Router = () => (
    <BrowserRouter>
        <Switch>
            <Route exact={true} path="/"><Redirect to="/home" /></Route>
            <Route exact={true} path="/signin" component={Signin} />
            <PrivateRoute exact={true} path="/home" component={Home} />
        </Switch>
    </BrowserRouter>
);

export default Router;
