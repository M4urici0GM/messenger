import React from 'react';

import {
    BackgroundContainer,
    WhiteContainer,
    ContentContainer
} from './styles';
import { Column } from '../Grid';
import LoadingOverlay from '../LoadingOverlay';

interface Props {
    children: React.ReactNode
    loading: boolean
}

const AppContainer: React.FC<Props> = (props) => {
    console.log('a');
    return (
        <BackgroundContainer>
            <LoadingOverlay loading={props.loading} />
            <WhiteContainer>
                <ContentContainer>
                    <Column>
                        {props.children}
                    </Column>
                </ContentContainer>
            </WhiteContainer>
        </BackgroundContainer>
    );
};

export default AppContainer;
