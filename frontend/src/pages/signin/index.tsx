import React from 'react';
import Undraw from 'react-undraw';
import { FaEnvelope } from 'react-icons/fa';

import Input from '../../components/Input';

const Signin: React.FC = () => {
    return (
        <div>
            <Input
                label="Teste"
                onChange={() => console.log('aa')}
                icon={
                    <FaEnvelope />
                }
            />
        </div>
    );
};

export default Signin;
