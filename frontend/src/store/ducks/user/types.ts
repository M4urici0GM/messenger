export interface IUserState {
    readonly user: IUser
    readonly loading: boolean
    readonly error: boolean
    readonly authenticated: boolean
};


export interface IUser {
    id: string,
    firstName: string,
    lastName: string,
    email: string,
}

export enum IUserTypes {
    LOAD_REQUEST = '@users/LOAD_REQUEST',
    LOAD_SUCCESS = '@users/LOAD_SUCCESS',
    LOAD_FAILURE = '@users/LOAD_FAILURE',
}
