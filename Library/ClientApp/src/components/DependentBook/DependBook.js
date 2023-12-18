import React, { Component } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './DependBook.css'
import ImagesComponent from './Images';
import { Scroll } from '../ScrollOfBooks/Scroll';
import { handleClickIsHeartedForDependBook } from '../BooksClickFunctions';

export class DependBook extends Component {
    static displayName = DependBook.name;

    constructor(props) {
        super(props);
        this.state = {
            bookId: sessionStorage.getItem('BookId') ? sessionStorage.getItem('BookId') : 1,
            book: null,
            loadingBook: true,
        };
    }

    async getBook() {
        try {
            const response = await fetch(`https://localhost:7165/books/GetDependBook?BookId=${this.state.bookId}`, {
                credentials: 'include'
            });

            if (response.ok) {
                const data = await response.json();
                if (data != null) {
                    this.setState({
                        book: data,
                        loadingBook: false,
                        error: null  // Reset error state on success
                    });
                } else {
                    console.error("No such book");
                    this.setState({
                        loadingBook: true,
                        error: 'No such book'
                    });
                }
            } else {
                console.error(`Failed to fetch book data. Status: ${response.status}`);
                this.setState({
                    loadingBook: false,
                    error: 'Failed to fetch book data'
                });
            }
        } catch (error) {
            console.error('Error during fetch:', error);
            this.setState({
                loadingBook: false,
                error: 'Error during fetch'
            });
        }
    }

    componentDidMount() {
        this.getBook();
    }

    renderBook(book) {
        return (
            <div className="Content">
                <div className='header'>
                    <div className='text'>{book.name}</div>
                    <div className='underline'></div>
                </div>
                <ImagesComponent images={book.imgs} />
                <div className="TextInSides mt-5">
                    <p>Is book hearted: </p>
                    <p>
                        <p className={`heart-button ${this.state.book.isHearted ? 'hearted' : ''}`}
                            onClick={() => handleClickIsHeartedForDependBook(book, (updatedBook, resolve) => this.setState({ book: updatedBook }, resolve))}
                        >
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-suit-heart-fill" viewBox="0 0 16 16">
                                <path d="M4 1c2.21 0 4 1.755 4 3.92C8 2.755 9.79 1 12 1s4 1.755 4 3.92c0 3.263-3.234 4.414-7.608 9.608a.513.513 0 0 1-.784 0C3.234 9.334 0 8.183 0 4.92 0 2.755 1.79 1 4 1" />
                            </svg>
                        </p>
                    </p>
                </div>
                <div className="TextInSides mt-5">
                    <p>Author: </p>
                    <p>{book.author}</p>
                </div>
                <div className="TextInSides mt-5">
                    <p>Types:</p>
                    <p>
                        {book.types[0]}
                        {book.types.slice(1).map((type) => (
                            ", " + type
                        ))}
                    </p>
                </div>
                <div className="TextInSides mt-5">
                    <p>Link to ya book: </p>
                    <p><a href={book.linkToYaBook}>Click to buy a book</a></p>
                </div>
                <div>
                    <p className="title mt-5">About book</p>
                    <p>{book.description}</p>
                </div>
                <div className="Characteristics">
                    <p className="title mt-5">Characteristics: </p>
                    <div className="TextInSides">
                        <p>Publishing houses:</p>
                        <p>
                            {book.publishingHouses[0]}
                            {book.publishingHouses.slice(1).map((publishingHouse) => (
                                ", " + publishingHouse
                            ))}
                        </p>
                    </div>
                    <div className="TextInSides">
                        <p>Languages:</p>
                        <p>
                            {book.languages[0]}
                            {book.languages.slice(1).map((language) => (
                                ", " + language
                            ))}
                        </p>
                    </div>
                    <div className="TextInSides">
                        <p>Years of publication:</p>
                        <p>
                            {book.yearsOfPublication[0]}
                            {book.yearsOfPublication.slice(1).map((year) => (
                                ", " + year
                            ))}
                        </p>
                    </div>
                    <div className="TextInSides">
                        <p>Format:</p>
                        <p>
                            {book.Format}
                        </p>
                    </div>
                    <div className="TextInSides">
                        <p>Type:</p>
                        <p>
                            {book.bookType}
                        </p>
                    </div>
                    <div className="TextInSides">
                        <p>Illustrations:</p>
                        <p>
                            {book.illustrations ? "Exist" : "No illustrations"}
                        </p>
                    </div>
                    <div className="TextInSides">
                        <p>Weight:</p>
                        <p>
                            {book.weight}
                        </p>
                    </div>
                    <div>
                        <p className="title mt-5">About author</p>
                        <p>{book.authorDescription}</p>
                    </div>
                    <div className='mt-5'>
                        <Scroll />
                    </div>
                </div>
            </div>);
    }

    render() {
        let contents = this.state.loadingBook
            ? <p><em>Loading...</em></p>
            : this.renderBook(this.state.book);

        return (
            <div className="Content">
                {contents}
            </div>
        );
    }
}