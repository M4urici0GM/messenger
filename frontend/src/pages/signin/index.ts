import { connect } from 'react-redux';

import { toggleLoadingStatus } from '../../store/ducks/app/actions';
import { GlobalState } from '../../store';
import SigninComponent from './signin';
import { IDispatchProps, IStateProps } from './types';

const mapStateToProps = (state: GlobalState): IStateProps => ({
    app: state.app,
});

const mapDispatchToProps: IDispatchProps = {
    toggleLoadingStatus,
};

export default connect(mapStateToProps, mapDispatchToProps)(SigninComponent);
