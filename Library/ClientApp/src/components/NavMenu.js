import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Input, ListGroup, ListGroupItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import axios from 'axios';
import { handleImageClick } from './BooksClickFunctions';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.handleSearch = this.handleSearch.bind(this);

        this.state = {
            collapsed: true,
            isAuthenticated: localStorage.getItem('user') !== null,
            searchValue: '',
            filteredBooks: []
        };
    }

    handleSearch(event) {
        console.log('handleSearch called');

        const searchValue = event.target.value;
        console.log('searchValue:', searchValue);


        if (searchValue.trim() === '') {
            this.setState({
                searchValue,
                filteredBooks: []
            });
            return;
        }

        axios.get(`https://localhost:7165/books/Search?query=${searchValue}`, { withCredentials: true })
            .then(response => {
                const filteredBooks = response.data;
                this.setState({
                    searchValue,
                    filteredBooks
                });
            })
            .catch(error => {
                console.error('Error during book search:', error);
            });
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    handleLogout = () => {
        axios.post('https://localhost:7165/api/Account/logout', {}, { withCredentials: true })
            .then(response => {
                if (response.data.message === 'User logged out successfully.') {
                    localStorage.removeItem('user');
                    this.setState({ isAuthenticated: false });
                    window.location.reload();
                }
            })
            .catch(error => {
                console.error('Error during logout:', error);
            });
    }

    render() {
        const { isAuthenticated, searchValue, filteredBooks } = this.state;

        return (
            <header>
                <Navbar className="navbar-expand-sm ng-white border-bottom box-shadow mb-3" container light>
                    <NavbarBrand tag={Link} to="/">Library</NavbarBrand>
                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <Input
                                    id="searchInput"
                                    type="text"
                                    placeholder="Search books"
                                    value={searchValue}
                                    onChange={this.handleSearch}
                                />
                                {filteredBooks.length > 0 && (
                                    <ListGroup
                                        className="search-results">
                                        {filteredBooks.map(book => (
                                            <ListGroupItem key={book.id} onClick={() => handleImageClick(book.id)}>
                                                {book.name}
                                            </ListGroupItem>
                                        ))}
                                    </ListGroup>
                                )}
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/show-books">Show books</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/addBookByYaBookUrl">Add new book</NavLink>
                            </NavItem>
                            {isAuthenticated ? (
                                <>
                                    <NavItem>
                                        <NavLink className="text-dark">Hello {JSON.parse(localStorage.getItem('user')).name}</NavLink>
                                    </NavItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/" onClick={this.handleLogout}>Logout</NavLink>
                                    </NavItem>
                                </>
                            ) : (
                                <>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                                    </NavItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/registation">Registration</NavLink>
                                    </NavItem>
                                </>
                            )}
                        </ul>
                    </Collapse>
                </Navbar>
            </header>
        );
    }
}
