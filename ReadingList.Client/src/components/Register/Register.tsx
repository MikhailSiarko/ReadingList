import * as React from 'react';
import globalStyles from '../../styles/global.css';
import PrimaryButton from '../PrimaryButton';

interface RegisterProps {
    onConfirmPasswordChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const Register: React.SFC<RegisterProps> = props => {
    return (
        <div>
            <div>
                <input type="email" name="email" placeholder="Email" required={true} />
            </div>
            <div>
                <input type="password" name="password" placeholder="Password" required={true} />
            </div>
            <div>
                <input type="password" name="confirmPassword" placeholder="Confirm Password" required={true}
                       onChange={props.onConfirmPasswordChange} />
                <span hidden={true} id={'validation-message'}>Passwords don't confirm</span>
            </div>
            <div>
                <PrimaryButton id={'submit-button'} disabled={true} className={globalStyles.disabled}
                        type="submit">Register</PrimaryButton>
            </div>
        </div>
    );
};

export default Register;
