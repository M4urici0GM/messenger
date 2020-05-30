import React from 'react';


import { StyledInput } from './styles';

interface InputProps {
    onChange(): void
    fullWidth?: boolean
}

const Input: React.FC<InputProps> = (props) => {
    return (
        <div>
            <StyledInput 
                onChange={props.onChange}
                fullWidth={props.fullWidth}
            />
        </div>
    );
};

Input.defaultProps = {
    fullWidth: false,
};

export default Input;
