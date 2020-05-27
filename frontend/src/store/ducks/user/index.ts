import { action } from 'typesafe-actions';
import { IUserState, UserTypes } from './types';


const initialState: IUserState = {
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



export const reducer = (state = initialState, action) => {
    switch(action.type) {

    }
};
