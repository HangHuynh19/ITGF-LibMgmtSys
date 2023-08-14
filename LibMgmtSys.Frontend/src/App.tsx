import React, { useEffect } from 'react';
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

const App = () => {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route path='/' element={<Root />}>
          <Route path='/' element={<HomePage />} />
          <Route path='/books/:id' element={<BookDetailPage />} />
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
