import { useEffect } from 'react';
import BookList from '../components/BookList';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { fetchAllBooks } from '../store/reducers/bookReducer';
import { Button } from '@mui/material';

const HomePage = () => {
  const books = useAppSelector((state) => state.bookReducer.books);
  const isAdmin = useAppSelector((state) => state.userReducer.isAdmin);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchAllBooks('asc'));
  }, [dispatch]);
  console.log('HomePage', books);

  return (
    <div className='home-page-container'>
      {isAdmin && (
        <Button
          className='home-page-container__create-book-btn'
          variant='contained'
          color='secondary'
          href='/books/add-book'
        >
          Add Book
        </Button>
      )}
      <BookList books={books} />;
    </div>
  );
};

export default HomePage;
