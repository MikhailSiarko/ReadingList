import * as React from 'react';

interface HelloProps {
    name: string | undefined;
}

const Hello = (props: HelloProps) => {
    return <h1>Hello, {props.name}!</h1>;
};

export default Hello;