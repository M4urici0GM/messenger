import React from 'react';

import {
    Container,
    BackgroundContainer,
} from './styles';

interface Props {
    children: React.ReactNode
}

const AppContainer: React.FC<Props> = (props) => {
    return (
        <BackgroundContainer>
            <Container>
                {props.children}
            </Container>
        </BackgroundContainer>
    );
};

export default AppContainer;
