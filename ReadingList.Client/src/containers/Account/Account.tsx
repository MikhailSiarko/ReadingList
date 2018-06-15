import * as React from 'react';
import { Route, RouteComponentProps } from 'react-router';
import Register from '../../components/Register';
import Login from '../../components/Login';
import { connect } from 'react-redux';
import { AuthenticationService, PrivateBookListService } from '../../services';
import { Credentials, AuthenticationData } from '../../store/actions/authentication';
import { Dispatch } from 'redux';
import { RootState } from '../../store/reducers';
import { RequestResult } from '../../models';
import AccountForm from '../../components/AccountForm';
import { isNullOrEmpty } from '../../utils';
import globalStyles from '../../styles/global.css';

interface AccountProps extends RouteComponentProps<any> {
    login: (credentials: Credentials) => Promise<void>;
    register: (credentials: Credentials) => Promise<void>;
}

class Account extends React.Component<AccountProps> {
    private static validateCredentials(email: string, password: string, confirmPassword?: string) {
        return isNullOrEmpty(email) || !isNullOrEmpty(password) || !isNullOrEmpty(confirmPassword);
    }

    public submitHandler = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const form = event.target as HTMLFormElement;
        const email = (form.elements.namedItem('email') as HTMLInputElement).value;
        const password = (form.elements.namedItem('password') as HTMLInputElement).value;
        const confirmPassword = form.elements.namedItem('confirmPassword') as HTMLInputElement;
        if(confirmPassword === null) {
            await this.submitLogin(email, password);
        } else {
            await this.submitRegister(email, password, confirmPassword.value);
        }
    }

    confirmPasswordChangeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        const confirmPasswordInput = event.target as HTMLInputElement;
        const form = confirmPasswordInput.form as HTMLFormElement;
        const passwordInput = form.elements.namedItem('password') as HTMLInputElement;
        const validationSpan = document.getElementById('validation-message') as HTMLSpanElement;
        if(passwordInput && confirmPasswordInput) {
            const submitButton = document.getElementById('submit-button') as HTMLButtonElement;
            if(confirmPasswordInput.value === passwordInput.value) {
                if(submitButton) {
                    submitButton.disabled = false;
                    submitButton.classList.remove(globalStyles.disabled);
                }
                confirmPasswordInput.classList.remove(globalStyles['invalid-input']);
                validationSpan.classList.remove(globalStyles['input-validation-message']);
            } else {
                if(submitButton) {
                    submitButton.disabled = true;
                    submitButton.classList.add(globalStyles.disabled);
                }
                confirmPasswordInput.classList.add(globalStyles['invalid-input']);
                validationSpan.classList.add(globalStyles['input-validation-message']);
            }
        }
    }

    render() {
        const account = (
            <AccountForm onSubmit={this.submitHandler} id={'account-form'}>
                <Route path="/account/register" 
                    component={() => <Register onConfirmPasswordChange={this.confirmPasswordChangeHandler}  />} />
                <Route path="/account/login" component={Login}/>
            </AccountForm>
        );
        return <div>{account}</div>;
    }

    private async submitLogin(email: string, password: string) {
        if(Account.validateCredentials(email, password)) {
            await this.props.login(new Credentials(email, password));
        }
    }
    private async submitRegister(email: string, password: string, confirmPassword: string) {
        if(Account.validateCredentials(email, password, confirmPassword)) {
            await this.props.register(new Credentials(email, password, confirmPassword));
        }
    }
}

function postRequestProcess(result: RequestResult<any>, ownProps: AccountProps) {
    if(result.isSucceed) {
        ownProps.history.push('/');
    } else {
        alert(result.errorMessage);
    }
}

async function postAuthProcess(dispatch: Dispatch<RootState>, result: RequestResult<AuthenticationData>,
        ownProps: AccountProps) {
    if(result.isSucceed) {
        const bookService = new PrivateBookListService(dispatch);
        const bookResult = await bookService.getList();
        postRequestProcess(bookResult, ownProps);
    } else {
        postRequestProcess(result, ownProps);
    }
}

function mapDispatchToProps(dispatch: Dispatch<RootState>, ownProps: AccountProps) {
    const authService = new AuthenticationService(dispatch);
    return {
        login: async (credentials: Credentials) => {
            const result = await authService.login(credentials);
            await postAuthProcess(dispatch, result, ownProps);
        },
        register: async (credentials: Credentials) => {
            const result = await authService.register(credentials);
            await postAuthProcess(dispatch, result, ownProps);
        }
    };
}

export default connect(null, mapDispatchToProps)(Account);