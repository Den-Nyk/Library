import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/HomePage/Home";
import { Login } from './components/Account/Login';
import { Registration } from './components/Account/Registration';

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/counter',
        element: <Counter />
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
    }
];

export default AppRoutes;
