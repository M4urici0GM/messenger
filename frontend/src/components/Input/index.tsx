import React from 'react';

import { 
    StyledInput, 
    StyledInputLabel,
    InputContainer,
    IconContainer,
} from './styles';

interface InputProps {
    onChange(): void
    fullWidth?: boolean
    type?: string
    label?: string
    icon?: React.ReactNode
}

const Input: React.FC<InputProps> = (props) => {
    return (
        <InputContainer>
            {props.label && (
                    <StyledInputLabel>
                        {props.label}
                    </StyledInputLabel>
                )
            }
            <StyledInput
                type={props.type}
                onChange={props.onChange}
                fullWidth={props.fullWidth}
            />
            {props.icon && (
                <IconContainer>
                    {props.icon}
                </IconContainer>
            )}
        </InputContainer>
    );
};

Input.defaultProps = {
    fullWidth: false,
    type: 'text',
};

export default Input;
