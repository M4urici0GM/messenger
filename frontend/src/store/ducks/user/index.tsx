import { IUserState, UserTypes } from './types';
import { Action } from 'redux';


const initialState: IUserState = {
    error: false,
    loading: false,
    authenticated: true,
    user: {
        id: '',
        firstName: '',
        lastName: '',
        email: '',
    }
};


export const userReducer = (state: IUserState = initialState, action: Action<UserTypes>) => {
    switch(action.type) {
        default: return state;
    }
};
