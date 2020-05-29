import { combineReducers, createStore, applyMiddleware } from 'redux';
import Thunk from 'redux-thunk';

import { IUserState } from './ducks/user/types';
import { userReducer } from './ducks/user';

export interface ApplicationState {
    userState: IUserState
}

const reducers = combineReducers<ApplicationState>({
    userState: userReducer,
});

const middlewares = [
    Thunk
];

const store = createStore(reducers, {}, applyMiddleware(...middlewares));

export default store;
