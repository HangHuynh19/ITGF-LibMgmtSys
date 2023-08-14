import { ImageListItem, ImageListItemBar } from '@mui/material';
import { Book } from '../interfaces/Book';
import { Link } from 'react-router-dom';

const BookItem = ({ book }: { book: Book }) => {
  const authorString = book.authorNames.join(', ');
  const randomInt = Math.floor(Math.random() * 15) + 1;

  return (
    <>
      <ImageListItem>
        <img
          src={require(`../assets/bookImages/book${randomInt}.jpeg?w=164&h=164&fit=crop&auto=format`)}
          alt={book.title}
          loading='lazy'
        />
        <Link to={`/books/${book.id}`}>
          <ImageListItemBar title={book.title} subtitle={authorString} />
        </Link>
      </ImageListItem>
    </>
  );
};

export default BookItem;
