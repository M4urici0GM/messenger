import { Action, Reducer } from 'redux';

import { IUserState, IUserTypes } from './types';

const INITIAL_STATE: IUserState = {
    error: false,
    loading: false,
    authenticated: false,
    user: {
        id: '',
        firstName: '',
        lastName: '',
        email: '',
    }
};


const userReducer: Reducer<IUserState> = (state = INITIAL_STATE, action) => {
    switch(action.type) {
        default: return state;
    }
};

export default userReducer;
