import React, { useState } from 'react';
import Undraw from 'react-undraw';
import { FaEnvelope, FaEye, FaEyeSlash, FaSignInAlt, FaGoogle } from 'react-icons/fa';

import Input from '../../components/Input';
import Button from '../../components/Button';

const Signin: React.FC = () => {

    const [ isPassShown, setIsPassShown ] = useState(false);

    const togglePassword = (): void => setIsPassShown(!isPassShown);

    return (
        <div>
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
            <Button
                content="Signin"
                dark
                fullWidth
                icon={
                    <FaSignInAlt />
                }
            />
            <Button
                content="Signin with google"
                transparent
                icon={
                    <FaGoogle />
                }
            />
        </div>
    );
};

export default Signin;
