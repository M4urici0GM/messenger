import { createStore, applyMiddleware, Store } from 'redux';
import Thunk from 'redux-thunk';

import RootReducer from './ducks/rootReducer';

import { IUserState } from './ducks/user/types';
import { IAppState } from './ducks/app/types';


export interface GlobalState {
    readonly user: IUserState
    readonly app: IAppState
}

const middlewares = [
    Thunk
];

const store: Store<GlobalState> = createStore(RootReducer, {}, applyMiddleware(...middlewares));

export default store;
