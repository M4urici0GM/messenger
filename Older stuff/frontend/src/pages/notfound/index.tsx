import React from 'react';
import Undraw from 'react-undraw';

import { Row, Column } from '../../components/Grid';
import { Header } from './styles';

const NotFound: React.FC = () => {

    return (
        <Row
            fullHeight
            justifyItems="center"
            alignItems="center"
        >
            <Column sm={12}>
                <Undraw name="notFound" />
                <Header>
                    Ops, nothing here.
                </Header>
            </Column>
        </Row>
    );
};

export default NotFound;
