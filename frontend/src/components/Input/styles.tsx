import styled, { ThemedStyledInterface } from 'styled-components';

import { Theme } from '../../store/ducks/app/types';

interface InputProps {
    fullWidth?: boolean
    theme: Theme
}

export const InputContainer = styled.div`
    display: flex;
    flex-direction: column;
`;

export const StyledInput = styled.input<InputProps>`
    width: ${props => props.fullWidth ? '100%' : '300px;'};
    border-radius: 5px;
    color: ${props => props.theme.primaryText};
    border: 1px solid ${(props) => props.theme.primaryDark };
    padding: 1em;
    font-size: 1em;
`;

export const StyledInputLabel = styled.label`
    margin-bottom: 5px;
`;

export const IconContainer = styled.div`
    
`;
