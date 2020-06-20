import React, { FunctionComponent } from 'react';
import Undraw from 'react-undraw';

import Fade from '../../components/Fade';
import SignupForm from './components/signupForm';

import { Row, Column, Container } from '../../components/Grid';
import { SigninButton } from './styles';



const SignUp: FunctionComponent = () => {

    return (
        <Fade>
            <Container>
                <Row className="d-flex align-items-center">
                    <Column sm={12} md={6}>
                        <Undraw name="signin" height="300" />
                    </Column>
                    <Column sm={12} md={6}>
                        <SignupForm onSignupFormSubmit={() => alert('aaa')} />
                        <Row className="d-flex justify-content-center">
                        <Column className="d-flex">
                                <SigninButton>
                                    Already have an account? Click here
                                </SigninButton>
                        </Column>
                        </Row>
                    </Column>
                </Row>
            </Container>
        </Fade>
    );
};

export default SignUp;
