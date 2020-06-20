import React, { useState } from 'react';
import { withFormik } from 'formik';
import {
    FaEnvelope,
    FaEye,
    FaSignInAlt,
    FaEyeSlash,
} from 'react-icons/fa';
import { toast } from 'react-toastify';


import { Row, Column, Container } from '../../../../components/Grid';
import Button from '../../../../components/Button';
import Input from '../../../../components/Input';

import {
    ComponentProps,
    FormProps,
    OwnProps,
} from './types';

const SignupForm: React.SFC<ComponentProps> = (props) => {
    const {
        values: {
            email,
            firstName,
            lastName,
            password,
            confirmPassword,
        },
        handleSubmit,
        setFieldValue,
        handleChange,
    } = props;

    const [isPasswordShown, setIsPasswordShown] = useState(false);

    const onTogglePasswordVisibilityButtonClick = () => setIsPasswordShown(!isPasswordShown);

    return (
        <form onSubmit={handleSubmit}>
            <Row>
                <Column sm={12} md={6} className="mt-3 pr-">
                    <Input
                        fullWidth
                        value={firstName}
                        label="Your first name"
                        id="firstName"
                        type="text"
                        onChange={handleChange}
                        placeholder="your@email.com"
                    />
                </Column>
                <Column sm={12} md={6} className="mt-3">
                    <Input
                        fullWidth
                        value={lastName}
                        id="lastName"
                        label="Your last name"
                        type="text"
                        onChange={handleChange}
                        placeholder="your@email.com"
                    />
                </Column>
            </Row>
            <Row className="mt-3">
                <Column>
                    <Input
                        label="Your best email"
                        type="email"
                        id="email"
                        value={email}
                        onChange={handleChange}
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
                        type={isPasswordShown ? 'text' : 'password'}
                        id="password"
                        onChange={handleChange}
                        placeholder="*******"
                        onIconClick={onTogglePasswordVisibilityButtonClick}
                        icon={
                            isPasswordShown
                                ? <FaEyeSlash />
                                : <FaEye />
                        }
                    />
                </Column>
            </Row>
            <Row className="mt-3">
                <Column>
                    <Input
                        label="Confirm your password"
                        type={isPasswordShown ? 'text' : 'password'}
                        id="confirmPassword"
                        onChange={handleChange}
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
                        onClick={handleSubmit}
                        icon={
                            <FaSignInAlt />
                        }
                    />
                </Column>
            </Row>
        </form>
    );
}

export default withFormik<OwnProps, FormProps>({
    mapPropsToValues: () => ({
        firstName: '',
        confirmPassword: '',
        email: '',
        lastName: '',
        password: '',
        isPasswordShown: false,
    }),
    handleSubmit: async (values, formikBag) => {
        const {
            firstName,
            lastName,
            email,
            password,
            confirmPassword,
        } = values;
    
        if (!firstName || !lastName || !email || !password || !confirmPassword) {
            toast.error('All fields are required!');
            return;
        } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email)) {
            toast.error('Invalid Email Address');
            return;
        } else if (password !== confirmPassword) {
            toast.error('Passwords doesn\'t match');
            return;
        }

        await formikBag.props.onSignupFormSubmit(values);
    },
})(SignupForm);
