import './Registration_Login.css';
import { Link } from 'react-router-dom';
import React, { Component } from 'react';
import axios from 'axios';

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            formData: {
                email: '',
                password: ''
            },
            errors: {},
            loginSuccessful: false,
        };
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState((prevState) => ({ formData: { ...prevState.formData, [name]: value } }));
    };

    isNullOrWhitespace = (value) => {
        return value === undefined || value === null || value.trim() === '';
    };

    handleSubmit = async (e) => {
        e.preventDefault();

        if (this.isNullOrWhitespace(this.state.formData.email) || this.isNullOrWhitespace(this.state.formData.password))
            return;

        try {
            const response = await axios.post('https://localhost:7165/api/Account/login', this.state.formData, { withCredentials: true });

            if (response.status === 200) {
                localStorage.setItem('user', JSON.stringify({ name: response.data.name }));
                window.location.reload();
                window.location.href = '/';
            }
        } catch (error) {
            // Registration failed
            if (error.response) {
                // The request was made and the server responded with an error status
                const serverErrors = error.response.data.errors;
                this.setState({ serverAnswer: serverErrors, loginSuccessful: false });
            } else if (error.request) {
                // The request was made but no response was received
                console.error('No response received from the server.');
            } else {
                // Something happened in setting up the request that triggered an Error
                console.error('Error during request setup:', error.message);
            }
        }
    };

    render() {
        const message = sessionStorage.getItem('errorMessage');
        sessionStorage.removeItem('errorMessage');

        const { serverAnswer } = this.state;

        return (
            <div className='loginRegisterContainer'>
                <div className='header'>
                    <div className='text'>Login</div>
                    <div className='underline'></div>
                </div>
                {serverAnswer && (
                    <div className='server-answer'>
                        <p>{serverAnswer}</p>
                    </div>
                )}
                {message && message === 'redirectToLogin' && (
                    <div className='redirect-message'>
                        Please login before liking photos.
                    </div>
                )}
                <div className='inputs'>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-envelope-at" viewBox="0 0 16 16">
                            <path d="M2 2a2 2 0 0 0-2 2v8.01A2 2 0 0 0 2 14h5.5a.5.5 0 0 0 0-1H2a1 1 0 0 1-.966-.741l5.64-3.471L8 9.583l7-4.2V8.5a.5.5 0 0 0 1 0V4a2 2 0 0 0-2-2zm3.708 6.208L1 11.105V5.383zM1 4.217V4a1 1 0 0 1 1-1h12a1 1 0 0 1 1 1v.217l-7 4.2z" />
                            <path d="M14.247 14.269c1.01 0 1.587-.857 1.587-2.025v-.21C15.834 10.43 14.64 9 12.52 9h-.035C10.42 9 9 10.36 9 12.432v.214C9 14.82 10.438 16 12.358 16h.044c.594 0 1.018-.074 1.237-.175v-.73c-.245.11-.673.18-1.18.18h-.044c-1.334 0-2.571-.788-2.571-2.655v-.157c0-1.657 1.058-2.724 2.64-2.724h.04c1.535 0 2.484 1.05 2.484 2.326v.118c0 .975-.324 1.39-.639 1.39-.232 0-.41-.148-.41-.42v-2.19h-.906v.569h-.03c-.084-.298-.368-.63-.954-.63-.778 0-1.259.555-1.259 1.4v.528c0 .892.49 1.434 1.26 1.434.471 0 .896-.227 1.014-.643h.043c.118.42.617.648 1.12.648Zm-2.453-1.588v-.227c0-.546.227-.791.573-.791.297 0 .572.192.572.708v.367c0 .573-.253.744-.564.744-.354 0-.581-.215-.581-.8Z" />
                        </svg>
                        <input
                            type='email'
                            placeholder="Enter email: "
                            className="form-control"
                            id="email"
                            name="email"
                            value={this.state.formData.email}
                            onChange={this.handleChange}
                            required
                            autoComplete="email"
                        ></input>
                    </div>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-key" viewBox="0 0 16 16">
                            <path d="M0 8a4 4 0 0 1 7.465-2H14a.5.5 0 0 1 .354.146l1.5 1.5a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0L13 9.207l-.646.647a.5.5 0 0 1-.708 0L11 9.207l-.646.647a.5.5 0 0 1-.708 0L9 9.207l-.646.647A.5.5 0 0 1 8 10h-.535A4 4 0 0 1 0 8zm4-3a3 3 0 1 0 2.712 4.285A.5.5 0 0 1 7.163 9h.63l.853-.854a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.793-.793-1-1h-6.63a.5.5 0 0 1-.451-.285A3 3 0 0 0 4 5z" />
                            <path d="M4 8a1 1 0 1 1-2 0 1 1 0 0 1 2 0z" />
                        </svg>
                        <input
                            type='password'
                            placeholder="Enter password: "
                            id="password"
                            name="password"
                            value={this.state.formData.password}
                            onChange={this.handleChange}
                            required
                            autoComplete="current-password"
                        ></input>
                    </div>
                    <div className='forgot-password'>
                        Forgot password?
                        <span> Click here!</span>
                    </div>
                    <div className='submit-container'>
                        <div className='submit' onClick={this.handleSubmit}>
                            Submit
                        </div>
                        <div className='submit'>
                            <Link to="/registation" className='submit'>
                                Sign up
                            </Link>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}