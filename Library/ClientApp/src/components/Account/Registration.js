import './Registration_Login.css'
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

export class Registration extends Component {
    constructor(props) {
        super(props);
        this.state = {
            formData: {
                name: '',
                surname: '',
                fatherName: '',
                dateOfBirth: '',
                email: '',
                password: '',
                confirmPassword: '',
            },
            errors: {},
        };
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState((prevState) => ({ formData: { ...prevState.formData, [name]: value } }));
    };

    checkSamePassword = () => {
        // Check if confirm password matches password
        const { password, confirmPassword } = this.state.formData;
        const errors = {};

        errors.confirmPassword = '';

        if (confirmPassword !== password) {
            errors.confirmPassword = 'Passwords do not match.';
        }

        this.setState((prevState) => ({
            errors: { ...prevState.errors, ...errors },
            passwordBlurred: true, // Set passwordBlurred to true after validation
        }));

        return errors.confirmPassword === '';
    }

    validatePassword = () => {
        const { password } = this.state.formData;
        const errors = {};

        errors.password = '';

        // Check if password is too short
        if (password.length < 6) {
            errors.password = 'Password must be at least 6 characters long. ';
        }

        // Check if password contains both letters and numbers
        if (!/(?=.*[A-Za-z])(?=.*\d)/.test(password)) {
            if (errors.password !== undefined) {
                errors.password += ' Password must contain both letters and numbers.'
            }
            else {
                errors.password = 'Password must contain both letters and numbers.'
            }
        }

        this.setState((prevState) => ({
            errors: { ...prevState.errors, ...errors },
            passwordBlurred: true, // Set passwordBlurred to true after validation
        }));

        return errors.password === '';
    };

    handleSubmit = async (e) => {
        e.preventDefault();

        // Validate passwords before submitting
        if (!this.validatePassword() || !this.checkSamePassword())
            return;

        try {
            const response = await axios.post('https://localhost:7165/api/Account/register', this.state.formData);

            if (response.status === 200) {
                localStorage.setItem('user', JSON.stringify({ name: response.data.name, email: response.data.email }));
                window.location.reload();
                window.location.href = '/';
            }
        } catch (error) {
            // Registration failed
            if (error.response) {
                // The request was made and the server responded with an error status
                const serverErrors = error.response.data.errors;
                this.setState({ serverAnswer: serverErrors, registrationSuccessful: false });
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
        const { errors, serverAnswer } = this.state;

        return (
            <div className='loginRegisterContainer'>
                <div className='header'>
                    <div className='text'>Sing up</div>
                    <div className='underline'></div>
                </div>
                {serverAnswer && (
                    <div className='server-answer'>
                        <p>{serverAnswer}</p>
                    </div>
                )}
                <div className='inputs'>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person" viewBox="0 0 16 16">
                            <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6m2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0m4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4m-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664z" />
                        </svg>
                        <input
                            type='text'
                            placeholder="Enter name: "
                            className="form-control"
                            id="name"
                            name="name"
                            value={this.state.formData.name}
                            onChange={this.handleChange}
                            required></input>
                    </div>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person" viewBox="0 0 16 16">
                            <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6m2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0m4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4m-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664z" />
                        </svg>
                        <input
                            type='text'
                            placeholder="Enter surname: "
                            className="form-control"
                            id="surname"
                            name="surname"
                            value={this.state.formData.surname}
                            onChange={this.handleChange}
                            required
                        ></input>
                    </div>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person" viewBox="0 0 16 16">
                            <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6m2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0m4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4m-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664z" />
                        </svg>
                        <input
                            type='text'
                            placeholder="Enter father name: "
                            className="form-control"
                            id="fatherName"
                            name="fatherName"
                            value={this.state.formData.fatherName}
                            onChange={this.handleChange}
                        ></input>
                    </div>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-calendar-event" viewBox="0 0 16 16">
                            <path d="M11 6.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5z" />
                            <path d="M3.5 0a.5.5 0 0 1 .5.5V1h8V.5a.5.5 0 0 1 1 0V1h1a2 2 0 0 1 2 2v11a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V3a2 2 0 0 1 2-2h1V.5a.5.5 0 0 1 .5-.5M1 4v10a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4z" />
                        </svg>
                        <input
                            type='date'
                            className="form-control"
                            id="dateOfBirth"
                            name="dateOfBirth"
                            value={this.state.formData.dateOfBirth}
                            onChange={this.handleChange}
                        ></input>
                    </div>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-envelope-at" viewBox="0 0 16 16">
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
                        ></input>
                    </div>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-key" viewBox="0 0 16 16">
                            <path d="M0 8a4 4 0 0 1 7.465-2H14a.5.5 0 0 1 .354.146l1.5 1.5a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0L13 9.207l-.646.647a.5.5 0 0 1-.708 0L11 9.207l-.646.647a.5.5 0 0 1-.708 0L9 9.207l-.646.647A.5.5 0 0 1 8 10h-.535A4 4 0 0 1 0 8zm4-3a3 3 0 1 0 2.712 4.285A.5.5 0 0 1 7.163 9h.63l.853-.854a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.793-.793-1-1h-6.63a.5.5 0 0 1-.451-.285A3 3 0 0 0 4 5z" />
                            <path d="M4 8a1 1 0 1 1-2 0 1 1 0 0 1 2 0z" />
                        </svg>
                        <input
                            type='password'
                            placeholder="Enter password: "
                            className={`form-control ${errors.password ? 'is-invalid' : ''}`}
                            id="password"
                            name="password"
                            value={this.state.formData.password}
                            onChange={this.handleChange}
                            onBlur={this.validatePassword}
                            required
                        ></input>
                        {errors.password && <div className="invalid-feedback">{errors.password}</div>}
                    </div>
                    <div className='input'>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-key" viewBox="0 0 16 16">
                            <path d="M0 8a4 4 0 0 1 7.465-2H14a.5.5 0 0 1 .354.146l1.5 1.5a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0L13 9.207l-.646.647a.5.5 0 0 1-.708 0L11 9.207l-.646.647a.5.5 0 0 1-.708 0L9 9.207l-.646.647A.5.5 0 0 1 8 10h-.535A4 4 0 0 1 0 8zm4-3a3 3 0 1 0 2.712 4.285A.5.5 0 0 1 7.163 9h.63l.853-.854a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.793-.793-1-1h-6.63a.5.5 0 0 1-.451-.285A3 3 0 0 0 4 5z" />
                            <path d="M4 8a1 1 0 1 1-2 0 1 1 0 0 1 2 0z" />
                        </svg>
                        <input
                            type='password'
                            placeholder="Enter password again: "
                            className={`form-control ${errors.confirmPassword ? 'is-invalid' : ''}`}
                            id="confirmPassword"
                            name="confirmPassword"
                            value={this.state.formData.confirmPassword}
                            onChange={this.handleChange}
                            onBlur={this.checkSamePassword}
                            required
                        ></input>
                        {errors.confirmPassword && <div className="invalid-feedback">{errors.confirmPassword}</div>}
                    </div>
                    <div className='forgot-password'>
                        Forgot password?
                        <span> Click here!</span>
                    </div>
                    <div className='submit-container'>
                        <div className='submit' onClick={this.handleSubmit}>
                            Submit
                        </div>
                        <Link to="/login" className='submit'>
                            Login
                        </Link>
                    </div>
                </div>
            </div>
        );
    }
}