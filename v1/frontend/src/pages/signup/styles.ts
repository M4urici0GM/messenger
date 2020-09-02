import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const FormContainer = styled.div`
    padding: 0px 30px;
    display: flex;
    flex-direction: column;
    align-content: center;
    & > * {
        margin-top: 15px;
    }
`;

export const OrHeader = styled.label`
    text-align: center;
    font-size: 1em;
    color: ${props => props.theme.primaryText};
`;

export const SigninButton = styled(Link).attrs({
    to: '/signin'
})`
    cursor: pointer;
    align-self: center;
    color: ${props => props.theme.primaryText};
    border: 0;
    width: 100%;
    font-size: 1em;
    background: transparent;
    text-decoration: none;
    text-align: center;
    &:focus {
        outline: none;
    }
`;