import { Box, ImageList } from '@mui/material';
import BookItem from './BookItem';
import { Book } from '../interfaces/Book';

const BookList = ({ books }: { books: Book[] }) => {
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
