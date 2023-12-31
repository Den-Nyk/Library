﻿import React, { Component } from 'react';
import './Scroll.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import ImgGreatGatsby from '../HomePage/Img/TheGreatHutsby.jpg';
import { handleImageClick ,handleClick } from '../BooksClickFunctions';

export class Scroll extends Component {
    static displayName = Scroll.name;

    constructor(props) {
        super(props);
        this.state = {
            books: [],
            loading: true,
        };
    }

    async getBooks() {
        const response = await fetch('https://localhost:7165/books/GetBooksOfAllTime', {
            credentials: 'include'
        });
        const data = await response.json();
        this.setState({
            books: data,
            loading: false
        });
    }

    componentDidMount() {
        this.getBooks();
    }

    renderImage = (book) => {
        if (!book.img || book.img.length === 0) {
            return <img className="img" alt="Img" onClick={() => handleImageClick(book.id)} src={ImgGreatGatsby} />;
        }
        return <img className="img" alt="Img" onClick={() => handleImageClick(book.id)} src={`data:image/jpeg;base64,${book.img}`} />;
    }

    renderBooks(books) {
        return (
            <div>
                <div className='text-wrapper mb-1'>Books of all time</div>
                <div className="images mb-5">
                    {books.map((book) => (
                        <div key={book.id} className="img-with-text">
                            <div className="text-wrapper">
                                {book.name}
                                <button
                                    className={`heart-button ${book.isHearted ? 'hearted' : ''}`}
                                    onClick={() => handleClick(book.id, this.state.books, (updatedBooks, resolve) => this.setState({ books: updatedBooks }, resolve))}
                                >
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-suit-heart-fill" viewBox="0 0 16 16">
                                        <path d="M4 1c2.21 0 4 1.755 4 3.92C8 2.755 9.79 1 12 1s4 1.755 4 3.92c0 3.263-3.234 4.414-7.608 9.608a.513.513 0 0 1-.784 0C3.234 9.334 0 8.183 0 4.92 0 2.755 1.79 1 4 1" />
                                    </svg>
                                </button>
                            </div>
                            {this.renderImage(book)}
                        </div>
                    ))}
                </div>
            </div>);
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderBooks(this.state.books);

        return (
            <div>
                {contents}
            </div>
        );
    }
}