import React, { Component } from 'react';
import './ShowBooks.css';
import NoImg from '../noImg.jpg'
import 'bootstrap/dist/css/bootstrap.min.css';
import { handleImageClick, handleClick } from '../BooksClickFunctions';

export class ShowBooks extends Component {
    static displayName = ShowBooks.name;

    constructor(props) {
        super(props);
        this.state = {
            books: [],
            page: 1,
            loadingBooks: true,
            isLastPage: false,
        };
    }

    async getBooks() {
        try {
            const response = await fetch(`https://localhost:7165/books/GetBooks?page=${this.state.page}&title=${"nothing"}`, {
                credentials: 'include'
            });
            if (response.ok) {
                const data = await response.json();
                if (data != null && data.books != null) {
                    this.setState({
                        books: data.books,
                        isLastPage: data.isLastPage,
                        loadingBooks: false,
                        error: null
                    });
                } else {
                    console.error("No books get");
                    this.setState({
                        loadingBooks: true,
                        error: "No books get"
                    })
                }
            } else {
                console.error(`Failed to fetch books data. Status: ${response.status}`);
                this.setState({
                    loadingBooks: true,
                    error: "Failed to fetch books data"
                });
            }
        } catch (error) {
            console.error('Error fetching books:', error);
            this.setState({
                loadingBooks: false,
                error: 'Error during fetch'
            });
        }
    }

    componentDidMount() {
        this.getBooks();
    }

    renderImage = (book) => {
        if (!book.img || book.img.length === 0) {
            return <img className="img" alt="Img" onClick={() => handleImageClick(book.id)} src={NoImg} />;
        }
        return <img className="img" alt="Img" onClick={() => handleImageClick(book.id)} src={`data:image/jpeg;base64,${book.img}`} />;
    }

    handleNextPage = () => {
        if (!this.state.isLastPage) {
            this.setState(
                (prevState) => ({
                    page: prevState.page + 1
                }),
                () => {
                    this.getBooks();
                }
            );
        }
    }

    handlePreviousPage = () => {
        if (this.state.page !== 1) {
            this.setState(
                (prevState) => ({
                    page: prevState.page - 1
                }),
                () => {
                    this.getBooks();
                }
            );
        }
    }

    renderBooks(books) {
        const chunkedBooks = [];
        for (let i = 0; i < books.length; i += 3) {
            chunkedBooks.push(books.slice(i, i + 3));
        }

        return (
            <div className="ShowBooks">
                <div className="header">
                    <div className='text'>Books</div>
                    <div className='underline'></div>
                </div>
                <div className="container mb-5">
                    {chunkedBooks.map((row, rowIndex) => (
                        <div key={rowIndex} className="row mt-3">
                            {row.map((book) => (
                                <div key={book.id} className="col d-flex justify-content-center align-items-center">
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
                                </div>
                            ))}
                        </div>
                    ))}
                </div>
                <div className="mt-5 mb-5 d-flex justify-content-center align-items-center">
                    {this.state.page > 1 && (
                        <button onClick={this.handlePreviousPage} className="ms-2 d-flex justify-content-center align-items-center">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-left" viewBox="0 0 16 16">
                                <path d="M10 12.796V3.204L4.519 8zm-.659.753-5.48-4.796a1 1 0 0 1 0-1.506l5.48-4.796A1 1 0 0 1 11 3.204v9.592a1 1 0 0 1-1.659.753z" />
                            </svg>
                        </button>
                    )}

                    <span className="me-2 ms-2">Page {this.state.page}</span>

                    {!this.state.isLastPage && (
                        <button onClick={this.handleNextPage} className="ms-2 d-flex justify-content-center align-items-center">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-right" viewBox="0 0 16 16">
                                <path d="M6 12.796V3.204L11.481 8zm.659.753 5.48-4.796a1 1 0 0 0 0-1.506L6.66 2.451C6.011 1.885 5 2.345 5 3.204v9.592a1 1 0 0 0 1.659.753z" />
                            </svg>
                        </button>
                    )}

                </div>

            </div>)
    }

    render() {
        let contents = this.state.loadingBooks
            ? <p><em>Loading...</em></p>
            : this.renderBooks(this.state.books);

        return (
            <div className="Content">
                {contents}
            </div>
        );
    }
}