import * as React from 'react';
import PrimaryButton from '../PrimaryButton';

interface LoginProps {
}

const Login: React.SFC<LoginProps> = () => {
    return (
        <div>
            <div>
                <input type="email" name="email" placeholder="Email" required={true} />
            </div>
            <div>
                <input type="password" name="password" placeholder="Password" required={true} />
            </div>
            <div>
                <PrimaryButton type="submit">Login</PrimaryButton>
            </div>
        </div>
    );
};

export default Login;