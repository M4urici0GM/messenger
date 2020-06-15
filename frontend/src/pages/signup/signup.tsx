import React, { FunctionComponent, useState, useEffect } from 'react';
import {
    FaEnvelope,
    FaEye,
    FaSignInAlt,
    FaEyeSlash,
} from 'react-icons/fa';
import Undraw from 'react-undraw';


import { Row, Column, Container } from '../../components/Grid';
import Button from '../../components/Button';
import Input from '../../components/Input';
import Fade from '../../components/Fade';
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
                        <Row>
                            <Column sm={12} md={6} className="mt-3 pr-">
                                <Input
                                    fullWidth
                                    label="Your first name"
                                    type="text"
                                    onChange={() => console.log('bb')}
                                    placeholder="your@email.com"
                                />
                            </Column>
                            <Column sm={12} md={6} className="mt-3">
                                <Input
                                    fullWidth
                                    label="Your last name"
                                    type="text"
                                    onChange={() => console.log('bb')}
                                    placeholder="your@email.com"
                                />
                            </Column>
                        </Row>
                        <Row className="mt-3">
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
                                    type="password"
                                    onChange={() => console.log('aa')}
                                    placeholder="*******"
                                    icon={
                                        <FaEye />
                                    }
                                />
                            </Column>
                        </Row>
                        <Row className="mt-3">
                            <Column>
                                <Input
                                    label="Confirm your password"
                                    type="password"
                                    onChange={() => console.log('cc')}
                                    placeholder="*******"
                                />
                            </Column>
                        </Row>
                        <Row className="mt-3">
                            <Column>
                                <Button
                                    content="Signin"
                                    dark
                                    fullWidth
                                    icon={
                                        <FaSignInAlt />
                                    }
                                />
                            </Column>
                        </Row>
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
