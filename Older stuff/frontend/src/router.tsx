
import React from 'react';
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom';


import PrivateRoute from './components/PrivateRoute';

import Home from './pages/home';
import Signin from './pages/signin';
import Signup from './pages/signup';
import NotFound from './pages/notfound';

const Router = () => (
    <BrowserRouter>
        <Switch>
            <Route exact={true} path="/"><Redirect to="/home" /></Route>
            <Route exact={true} path="/signin" component={Signin} />
            <Route exact={true} path="/signup" component={Signup} />
            <PrivateRoute exact={true} path="/home" component={Home} />
            <Route path="*" component={NotFound} />
        </Switch>
    </BrowserRouter>
);

export default Router;
