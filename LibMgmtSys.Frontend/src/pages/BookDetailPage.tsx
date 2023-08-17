import { useParams } from 'react-router-dom';
import BookDetail from '../components/BookDetail';
import useAppSelector from '../hooks/useAppSelector';
import { Typography } from '@mui/material';

const BookDetailPage = () => {
  const id = useParams<{ id: string }>().id;
  const book = useAppSelector((state) =>
    state.bookReducer.books.find((book) => book.id === id)
  );
  
  return (
    <>
      {book ? (
        <BookDetail book={book} />
      ) : (
        <Typography variant='h4' align='center'>
          Book not found!
        </Typography>
      )}
    </>
  );
};

export default BookDetailPage;
