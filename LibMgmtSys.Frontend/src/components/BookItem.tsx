import { ImageListItem, ImageListItemBar } from '@mui/material';
import { Book } from '../interfaces/Book';

const BookItem = ({ book }: { book: Book }) => {
  const authorString =
    book.authors.length > 0 ? book.authors.join(', ') : book.authors;

  return (
    <>
      <ImageListItem>
        <ImageListItemBar title={book.title} subtitle={authorString} />
      </ImageListItem>
    </>
  );
};
