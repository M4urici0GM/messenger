import styled from 'styled-components';
import { Container, Row } from '../Grid';

export const BackgroundContainer = styled.div`
    display: flex;
    flex:1;
    background-color: #F2F4F8;
    justify-content: center;
    align-items: center;
`;

export const WhiteContainer = styled(Container).attrs({
    className: 'd-flex w-100 justify-content-center'
})`
    background-color: #ffff;
    width: 90%;
    min-width: 600px;
    min-height: 600px;
    border-radius: 10px;
    box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
`;

export const ContentContainer = styled(Row).attrs({
    className: 'd-flex align-self-center w-100'
})``;