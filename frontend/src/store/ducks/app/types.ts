
/**
 * Data Types
 */
export interface Theme {
    mode: string
    primaryColor: string
    primaryText: string
    primaryDark: string
    secondaryColor: string
    secondaryText: string
    backgroundColor: string
};

export enum AvailableThemes {
    LIGHT = 'LIGHT',
};

/**
 * State type
 */
export interface IAppState {
    readonly themeMode: Theme
    readonly loading: boolean
    readonly error: false
}


/**
 * Action Types
 */
export enum AppTypes {
    TOGGLE_THEME = '@app/TOGGLE_THEME',
}

