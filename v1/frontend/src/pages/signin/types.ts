import { IAppState } from '../../store/ducks/app/types';

export interface IStateProps {
    app: IAppState
}

export interface IDispatchProps {
    toggleLoadingStatus(): void
};
