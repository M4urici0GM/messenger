import styled, { ThemedStyledInterface } from 'styled-components';

interface InputProps { }

interface IconProps {
    color?: string
}

interface InputContainerProps {
    fullWidth?: boolean
    width?: number
}

export const InputContainer = styled.div<InputContainerProps>`
    display: flex;
    flex-direction: column;
    width: ${props => (!props.fullWidth && !props.width)
        ? 'inherit'
        : `${props.width}px`
    };
`;

export const StyledInput = styled.input<InputProps>`
    color: ${props => props.theme.primaryText};
    font-size: 1em;
    border: 0;
    flex: 10;
    padding: 0px 5px;
    &:focus {
        outline: none;
    }
`;

export const StyledInputLabel = styled.label`
    font-size: 1em;
    margin-bottom: 3px;
`;

export const IconContainer = styled.div`
    display: flex;
    flex-direction: row !important;
    align-items: center;
    justify-content: space-evenly;
    border-radius: 5px;
    padding: 10px 10px;
    border: 2px solid ${(props) => props.theme.primaryDark };
`;

export const Icon = styled.div<IconProps>`
    display: flex;
    flex: 1;
    align-content: center;
    justify-content: center;
    & svg {
        font-size: 1.5em;
        color: ${props => props.color ? props.color : props.theme.primaryText};
        opacity: .5;
        cursor: pointer;
    }
`;
