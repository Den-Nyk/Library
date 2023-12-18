import React, { Component } from 'react';
import axios from 'axios';
import '../Account/Registration_Login.css';

export class AddBookByYaBookUrl extends Component {
    static displayName = AddBookByYaBookUrl.name;

    constructor(props) {
        super(props);

        this.state = {
            formData: '',
            bookAdd: '',
            errorMessage: ''
        };
    }

    handleInputChange = (event) => {
        this.setState({ formData: event.target.value });
    };

    handleSubmit = (event) => {
        event.preventDefault(); // Prevents the default form submission behavior

        const { formData } = this.state;

        axios.post('https://localhost:7165/FillDbBook/FillByYaBookUrl', { requestData: formData }, { withCredentials: true })
            .then(response => {
                if (response.status === 200) {
                    this.setState({ bookAdd: 'BookAddOk' });
                    this.setState({ errorMessage: '' });
                }
            })
            .catch(error => {
                if (error.response && error.response.status === 400) {
                    this.setState({
                        errorMessage: 'SuchBookExist'
                    });
                    this.setState({ bookAdd: '' });
                    console.error('Error during request:', this.state.error.response.data.error);
                } else if (error.response && error.response.status === 404) {
                    this.setState({ errorMessage: 'redirectToLoginAdminOnly' });
                    sessionStorage.setItem('errorMessage', this.state.errorMessage);
                    window.location.href = '/login';
                } else {
                    console.error('Error during request:', error);
                    this.setState({ errorMessage: 'Error during request: ' + error });
                }
            });
    };

    render() {
        const { bookAdd, errorMessage } = this.state;
  
        return (
            <div className='loginRegisterContainer'>
                <div className='header'>
                    <div className='header'>
                        <div className='text'>Add Book</div>
                        <div className='underline'></div>
                    </div>
                    {errorMessage && errorMessage === 'SuchBookExist' &&(
                        <div className='server-answer'>
                            Error:  Such book already exist
                        </div>
                    )}
                    {bookAdd && bookAdd === 'BookAddOk' && (
                        <div className='redirect-message'>
                            Book add. If you want to add a new book, enter a new URL.
                        </div>
                    )}
                    <div className='inputs'>
                        <div className='input'>
                            <input
                                type="text"
                                placeholder="Enter ya book URL"
                                value={this.state.formData}
                                id="url"
                                name="url"
                                onChange={this.handleInputChange}
                                required
                            ></input>
                        </div>
                        <div className='submit-container'>
                            <div className='submit' onClick={this.handleSubmit}>
                                Submit
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
