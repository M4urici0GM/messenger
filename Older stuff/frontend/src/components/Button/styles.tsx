import styled from 'styled-components';

interface ButtonProps {
    fullWidth?: boolean
    dark?: boolean
    transparent?: boolean
};

export const IconContainer = styled.div`
    & svg {
        font-size: 1.5em;
        margin-right: 9px;
    }
`;

export const StyledButton = styled.button<ButtonProps>`
    display: flex;
    flex: 1;
    flex-direction: row;
    border: 0;
    width: ${props => props.fullWidth ? '100%' : 'max-content'};
    text-align: center;
    justify-content: center;
    cursor: pointer;
    padding: 10px;
    font-size: 1em;
    border-radius: 5px;
    align-items: center;
    font-weight: 400;
    color: ${props => props.transparent ? props.theme.primaryText : 'white'};
    background-color: ${props => props.transparent 
        ? 'transparent' 
        : props.dark
            ? props.theme.primaryDark
            : props.theme.primaryColor
    };
    border: ${props => props.transparent ? `2px solid ${props.theme.primaryDark}` : 'none' };
    &:focus {
        outline: none;
    }
`;
