import React from 'react';
// import Test from 'react-loadingg/lib/JumpCircleLoading';

import { LoadingOverlayProps } from './types';
import { LoadingContainer, LoadingComponent } from './styles';

const LoadingOverlay: React.FC<LoadingOverlayProps> = ({ loading }) => {
    return (
        <>
            {loading && (
                <LoadingContainer>
                    <LoadingComponent>
                        <div />
                        <div />
                        <div />
                        <div />
                    </LoadingComponent>
                </LoadingContainer>
            )}
        </>
    );
};

LoadingOverlay.defaultProps = {
    loading: false,
};

export default LoadingOverlay;
