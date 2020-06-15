import styled from 'styled-components';
import { Container as BSContainer, Row as BSRow, Col } from 'react-bootstrap';

export const Row = styled(BSRow)``;

export const Container = styled(BSContainer)``;

export const Column = styled(Col)``; 


// import { ColumnProps, RowProps } from './types';
// function getWidthString(span: number): string {
//     if (!span) return '';

//     let width = span/12 * 100;
//     return `width: ${width}% !important`;
// }

// export const Row = styled.div<RowProps>`
//     width: 100%;
//     ${({ fullHeight }) => fullHeight && 'height: 100%;'}
//     ${(({ display }) => display && `display: ${display};`)}
//     ${({ justifyItems }) => justifyItems && `justify-items: ${justifyItems};`}
//     ${({ alignItems }) => alignItems && `align-items: ${alignItems};`}
//     &::after {
//         clear: both;
//         content: "";
//         display: table;
//     }
// `;

// export const Column = styled.div<ColumnProps>`
//     float: left;
//     ${({ spacing }) => spacing && `padding: 0 ${spacing}px 0 ${spacing}px;`}
//     ${({ xs }) => (xs ? getWidthString(xs) : 'width: 100%;')};

//     @media only screen and (min-width: 576px) {
//         ${({ sm }) => (sm && getWidthString(sm))};
//         ${({ spacing }) => spacing && 'padding: 0;'}
//     }

//     @media only screen and (min-width: 768px) {
//         ${({ md }) => (md && getWidthString(md))};
//         ${({ spacing }) => spacing && 'padding: 0;'}
//     }

//     @media only screen and (min-width: 992px) {
//         ${({ lg }) => (lg && getWidthString(lg))};
//     }

//     @media only screen and (min-width: 1200px) {
//         ${({ xg }) => (xg && getWidthString(xg))};
//     }
// `;

