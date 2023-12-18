import { AddBookByYaBookUrl } from "./components/AddBook/AddBookByYaBookUrl";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/HomePage/Home";
import { Login } from './components/Account/Login';
import { Registration } from './components/Account/Registration';
import { DependBook } from './components/DependentBook/DependBook'
import { ShowBooks } from './components/AllBooks/ShowBooks'

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/addBookByYaBookUrl',
        element: <AddBookByYaBookUrl />
    },
    {
        path: '/fetch-data',
        element: <FetchData />
    },
    {
        path: 'login',
        element: <Login />
    },
    {
        path: '/registation',
        element: <Registration />
    },
    {
        path: '/depend-book',
        element: <DependBook />
    },
    {
        path: '/show-books',
        element: <ShowBooks />
    }
];

export default AppRoutes;
