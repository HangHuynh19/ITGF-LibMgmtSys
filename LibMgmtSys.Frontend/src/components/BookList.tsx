import { Box, ImageList } from '@mui/material';
import BookItem from './BookItem';
import { Book } from '../interfaces/Book';
import { useEffect } from 'react';
import useAppDispatch from '../hooks/useAppDispatch';
import { fetchAllBooks } from '../store/reducers/bookReducer';

const BookList = ({
  books,
  sortingOrder,
  searchTerm,
}: {
  books: Book[];
  sortingOrder: string;
  searchTerm: string;
}) => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(
      fetchAllBooks({
        sortingOrder: sortingOrder,
        searchTerm: searchTerm,
      })
    );
  }, [dispatch, searchTerm, sortingOrder]);

  return (
    <Box sx={{ height: '80vh', overflowY: 'scroll' }}>
      <ImageList cols={4} sx={{ width: '100%', height: '100%' }}>
        {books.map((book) => (
          <BookItem key={book.id} book={book} showAddToCart={true} />
        ))}
      </ImageList>
    </Box>
  );
};

export default BookList;
