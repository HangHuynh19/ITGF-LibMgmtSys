import { ThemeProvider } from '@mui/material';
import globalTheme from './theme/globalTheme';
import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
} from 'react-router-dom';
import Root from './pages/Root';
import HomePage from './pages/HomePage';
import BookDetailPage from './pages/BookDetailPage';
import './styles/styles.scss';
import ProfilePage from './pages/ProfilePage';
import CartPage from './pages/CartPage';
import CreateBookPage from './pages/CreateBookPage';
import UpdateBookPage from './pages/UpdateBookPage';

const App = () => {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route path='/' element={<Root />}>
          <Route path='/' element={<HomePage />} />
          <Route path='/books/add-book' element={<CreateBookPage />} />
          <Route path='/books/:id' element={<BookDetailPage />} />
          <Route path='/books/:id/edit' element={<UpdateBookPage />} />
          <Route path='/customers/profile' element={<ProfilePage />} />
          <Route path='/cart' element={<CartPage />} />
        </Route>
      </>
    )
  );

  return (
    <ThemeProvider theme={globalTheme}>
      <RouterProvider router={router} />
    </ThemeProvider>
  );
};

export default App;
