import * as React from 'react';
import Button from '../Button';
import globalStyles from '../../styles/global.css';

interface LoginProps {
}

const Login: React.SFC<LoginProps> = () => (
    <div>
        <div>
            <input 
                className={globalStyles.shadowed} 
                type="email" 
                name="email" 
                placeholder="Email" 
                required={true} 
            />
        </div>
        <div>
            <input 
                className={globalStyles.shadowed} 
                type="password" 
                name="password" 
                placeholder="Password" 
                required={true} 
            />
        </div>
        <div>
            <Button type={'submit'}>Login</Button>
        </div>
    </div>
);

export default Login;