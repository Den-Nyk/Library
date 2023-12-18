import axios from 'axios';

export const handleImageClick = async (bookId) => {
    sessionStorage.setItem('BookId', bookId);
    window.location.href = `/depend-book`;
};

export const handleClick = async (bookId, books, setBooksState) => {
    let isHearted = books.find((b) => b.id === bookId).isHearted;
    isHearted = !isHearted;

    try {
        const response = await axios.post(
            'https://localhost:7165/books/UpdateHeartedStatus',
            { bookId, isHearted },
            { withCredentials: true }
        );

        if (response.status === 200) {
            const updatedBooks = books.map((book) =>
                book.id === bookId ? { ...book, isHearted: !book.isHearted } : book
            );

            return new Promise((resolve) => setBooksState(updatedBooks, resolve));
        }
    } catch (error) {
        if (error.response && error.response.status === 404) {
            const errorMessage = 'redirectToLogin';
            sessionStorage.setItem('errorMessage', errorMessage);
            window.location.href = '/login';
        } else {
            console.error('Error during request:', error);
        }
    }
};

export const handleClickIsHeartedForDependBook = async (book, setBookState) => {
    try {
        const response = await axios.post(
            'https://localhost:7165/books/UpdateHeartedStatus',
            {
                bookId: book.id,
                isHearted: !book.isHearted
            },
            { withCredentials: true }
        );

        if (response.status === 200) {
            const updatedBook = { ...book }
            updatedBook.isHearted = !updatedBook.isHearted;

            return new Promise((resolve) => setBookState(updatedBook, resolve));
        }
    } catch (error) {
        if (error.response && error.response.status === 404) {
            const errorMessage = 'redirectToLogin';
            sessionStorage.setItem('errorMessage', errorMessage);
            window.location.href = '/login';
        } else {
            console.error('Error during request:', error);
        }
    }
};