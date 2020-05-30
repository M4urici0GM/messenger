import styled, { ThemedStyledInterface } from 'styled-components';

import { Theme as AppTheme } from '../../store/ducks/app/types';

interface InputProps {
    fullWidth?: boolean
}

export const StyledInput = styled.input<InputProps>`
    width: ${props => props.fullWidth ? '100%' : '300px;'};
    border-radius: 5px;
    border: 1px solid ${(props) => props.theme.primaryDark };
    padding: 1em;
    font-size: 1em;
`;



