import styled from 'styled-components';

export const LoadingContainer = styled.div`
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: rgba(0, 0, 0, 0.3);
    position: absolute;
    z-index: 100;
    border-radius: 10px;
`;

export const LoadingComponent = styled.div`
    display: inline-block;
    position: relative;
    width: 80px;
    height: 80px;

    & div {
        position: absolute;
        top: 33px;
        width: 13px;
        height: 13px;
        border-radius: 50%;
        background: #fff;
        animation-timing-function: cubic-bezier(0, 1, 1, 0);
    }

    & div:nth-child(1) {
        left: 8px;
        animation: lds-ellipsis1 0.6s infinite;
    }

    & div:nth-child(2) {
        left: 8px;
        animation: lds-ellipsis2 0.6s infinite;
    }

    & div:nth-child(3) {
        left: 32px;
        animation: lds-ellipsis2 0.6s infinite;
    }

    & div:nth-child(4) {
        left: 56px;
        animation: lds-ellipsis3 0.6s infinite;
    }

    @keyframes lds-ellipsis1 {
        0% {
            transform: scale(0);
        }
        100% {
            transform: scale(1);
        }
    }
    @keyframes lds-ellipsis3 {
        0% {
            transform: scale(1);
        }
        100% {
            transform: scale(0);
        }
    }
    @keyframes lds-ellipsis2 {
        0% {
            transform: translate(0, 0);
        }
        100% {
            transform: translate(24px, 0);
        }
    }
`;