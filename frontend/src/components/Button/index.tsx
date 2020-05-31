import React, { FunctionComponent } from 'react';
import { connect } from 'react-redux';

import { GlobalState } from '../../store';
import { Theme } from '../../store/ducks/app/types';

import {
    StyledButton,
    IconContainer,
} from './styles';

interface StateProps {
    theme: Theme
};

interface OwnProps {
    dark?: boolean
    transparent?: boolean
    width?: number
    content?: string
    icon?: React.ReactNode
    fullWidth?: boolean
    onClick?(): void
}

type Props = OwnProps & StateProps;

const Button: FunctionComponent<Props> = (props) => {
    return (
        <StyledButton
            onClick={props.onClick}
            fullWidth={props.fullWidth}
            transparent={props.transparent}
            dark={props.dark}
        >
            {props.icon && (
                <IconContainer>
                    {props.icon}
                </IconContainer>
            )}
            {props.content}
        </StyledButton>
    );
};

Button.defaultProps = {
    content: 'Button',
    fullWidth: false,
    onClick: () => {},
}

const mapStateToProps = (state: GlobalState): StateProps => ({
    theme: state.app.themeMode,
});

export default connect(mapStateToProps)(Button);