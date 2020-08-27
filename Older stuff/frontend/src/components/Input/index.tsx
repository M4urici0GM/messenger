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
    width?: number
    label?: string
    icon?: React.ReactNode
    iconColor?: string
    style?: any
    onIconClick?(): void
}

const Input: React.FC<InputProps & React.HTMLProps<HTMLInputElement>> = (props) => {
    const {
        label,
        icon,
        onIconClick,
        onChange,
        id,
        placeholder,
        type,
    } = props;

    return (
        <InputContainer
            fullWidth={props.fullWidth}
            width={props.width}
            style={props.style}
        >
            {props.label && (
                    <StyledInputLabel>
                        {props.label}
                    </StyledInputLabel>
                )
            }
            <IconContainer>
                <StyledInput
                    onChange={onChange}
                    id={id}
                    placeholder={placeholder}
                    type={type}
                />               
                {props.icon && (
                    <Icon onClick={onIconClick}>
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
    style: {},
    onBlur: () => {},
    onFocus: () => {},
    onChange: () => {},
    onIconClick: () => {},
};

export default Input;
