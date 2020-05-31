import React from 'react';

import { 
    StyledInput, 
    StyledInputLabel,
    InputContainer,
    IconContainer,
    Icon,
} from './styles';

interface InputProps {
    fullWidth?: boolean
    type?: string
    width?: number
    placeholder?: string
    label?: string
    icon?: React.ReactNode
    iconColor?: string
    onBlur?(): void
    onFocus?(): void
    onChange?(): void
}

const Input: React.FC<InputProps> = (props) => {
    return (
        <InputContainer
            fullWidth={props.fullWidth}
            width={props.width}
        >
            {props.label && (
                    <StyledInputLabel>
                        {props.label}
                    </StyledInputLabel>
                )
            }
            <IconContainer>
                <StyledInput
                    type={props.type}
                    onChange={props.onChange}
                    placeholder={props.placeholder}
                />               
                {props.icon && (
                    <Icon>
                        {props.icon}
                    </Icon>
                )}
            </IconContainer>
        </InputContainer>
    );
};

Input.defaultProps = {
    fullWidth: true,
    type: 'text',
    placeholder: '',
    onBlur: () => {},
    onFocus: () => {},
    onChange: () => {},
};

export default Input;
