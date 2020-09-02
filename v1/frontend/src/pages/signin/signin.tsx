import React, { useState } from 'react';
import Undraw from 'react-undraw';
import { 
    FaEnvelope, 
    FaEye, 
    FaEyeSlash, 
    FaSignInAlt, 
    FaGoogle,
    FaFacebook,
} from 'react-icons/fa';
import { toast } from 'react-toastify';

import Input from '../../components/Input';
import Button from '../../components/Button';
import Fade from '../../components/Fade';
import { Row, Column, Container } from '../../components/Grid';
import {
    OrHeader,
    SignupButton,
} from './styles';

import { IDispatchProps, IStateProps } from './types';

type OwnProps = IDispatchProps & IStateProps;

const Signin: React.FC<OwnProps> = (props) => {
    const [ isPassShown, setIsPassShown ] = useState(false);

    const onSignButtonClick = async () => await props.toggleLoadingStatus();

    const onSignUpWithGoogleButtonClick = () => toast.error('Coming soon');

    const togglePassword = (): void => setIsPassShown(!isPassShown);
    
    return (
        <Fade>
            <Container>
                <Row className="d-flex align-items-center">
                    <Column sm={12} md={6}>
                        <Undraw name="authenticate" height="300" />
                    </Column>
                    <Column sm={12} md={6}>
                        <Row>
                            <Column>
                                <Input
                                    label="Your best email"
                                    type="email"
                                    onChange={() => console.log('bb')}
                                    placeholder="your@email.com"
                                    icon={
                                        <FaEnvelope />
                                    }
                                />
                            </Column>
                        </Row>
                        <Row className="mt-3">
                            <Column>
                                <Input
                                    label="Your password"
                                    type={isPassShown ? 'text' : 'password'}
                                    onChange={() => console.log('aa')}
                                    placeholder="*******"
                                    icon={
                                        (isPassShown) ? (
                                            <FaEyeSlash onClick={togglePassword} />
                                        ) : (
                                            <FaEye onClick={togglePassword} />
                                        )
                                    }
                                />
                            </Column>
                        </Row>
                        <Row className="mt-3">
                            <Column>
                                <Button
                                    content="Signin"
                                    dark
                                    fullWidth
                                    onClick={onSignButtonClick}
                                    icon={
                                        <FaSignInAlt />
                                    }
                                />
                            </Column>
                        </Row>
                        <Row className="mt-3">
                            <Column className="d-flex flex-column">
                                <OrHeader>Or: </OrHeader>
                                <Button
                                    content="Signin with google"
                                    transparent
                                    fullWidth
                                    onClick={onSignUpWithGoogleButtonClick}
                                    icon={
                                        <FaGoogle />
                                    }
                                />
                            </Column>
                        </Row>
                        <Row className="mt-3 ">
                        <Column>
                                <Button
                                    content="Signin with Facebook"
                                    transparent
                                    fullWidth
                                    icon={
                                        <FaFacebook />
                                    }
                                />
                        </Column>
                        </Row>
                        <Row className="d-flex justify-content-center">
                            <Column className="d-flex">
                                <SignupButton>
                                    Don't have an account yet? Click here
                                </SignupButton>
                            </Column>
                        </Row>
                    </Column>
                </Row>
            </Container>
        </Fade>
    );
};

export default Signin;
