import { Reducer } from 'redux';


import { IAppState, AppTypes, Theme } from './types';

const defaulTheme: Theme = {
    mode: 'light',
    backgroundColor: '',
    primaryColor: '#5E81AC',
    primaryText: '#3B4252',
    primaryDark: '#3F3D56',
    secondaryColor: '',
    secondaryText: '',
}

const INITIAL_STATE: IAppState = {
    themeMode: defaulTheme,
    error: false,
    loading: false,
};

const appReducer: Reducer<IAppState> = (state = INITIAL_STATE, action) => {
    switch(action.type) {
        case AppTypes.TOGGLE_LOADING_STATUS:
            return {
                ...state,
                loading: !state.loading,
            }
        default: return state;
    }
};

export default appReducer;