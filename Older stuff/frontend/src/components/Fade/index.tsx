import React from 'react';

import { CSSTransition } from 'react-transition-group';

const Fade: React.FC = (props) => {
    return (
        <CSSTransition
            appear
            in
            classNames="component-transition"
            timeout={{
                enter: 500,
                exit: 500,
            }}
            mountOnEnter
            unmountOnExit
        >
            {props.children}
        </CSSTransition>
    );
};

export default Fade;
