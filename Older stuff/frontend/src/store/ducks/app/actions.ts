import { action } from 'typesafe-actions';

import { AppTypes, AvailableThemes } from './types';

export const toggleTheme = (theme: AvailableThemes) => action(AppTypes.TOGGLE_THEME, { theme });

export const toggleLoadingStatus = () => action(AppTypes.TOGGLE_LOADING_STATUS);
