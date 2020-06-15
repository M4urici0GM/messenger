import { combineReducers } from 'redux';


import UserReducer from './user';
import AppReducer from './app';

const rootReducer = combineReducers({
    user: UserReducer,
    app: AppReducer
});

export default rootReducer;