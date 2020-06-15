import React from 'react';
import { Route, Redirect, RouteProps } from 'react-router-dom';
import { connect } from 'react-redux';

import { IUserState } from '../../store/ducks/user/types'
import { GlobalState } from '../../store';

interface StateProps {
    userState: IUserState
}

interface ComponentProps extends RouteProps {
    component: any
}

type Props = StateProps & ComponentProps;

const PrivateRoute: React.FC<Props> = (props) => {
    const {
        component: Component,
        userState,
        ...rest
    } = props;
    return (
        <Route
            {...rest}
            render={(routeProps) => 
                userState.authenticated 
                ? ( <Component {...routeProps} /> )
                : (
                    <Redirect
                        to={{
                            pathname: '/signin',
                            state: { from: routeProps.location },
                        }}
                    />
                )
            }
        />
    );
};

const mapStateToProps = (state: GlobalState): StateProps  => ({
    userState: state.user,
});

export default connect(mapStateToProps)(PrivateRoute);
