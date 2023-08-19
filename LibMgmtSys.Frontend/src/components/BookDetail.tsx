import { Book } from '../interfaces/Book';
import { ImageListItem, Typography } from '@mui/material';

const BookDetail = ({ book }: { book: Book }) => {
  const randomInt = Math.floor(Math.random() * 15) + 1;

  return (
    <div id='book-detail-content'>
      <img
        id='book-detail-content__img'
        src={require(`../assets/bookImages/book${randomInt}.jpeg?w=164&h=164&fit=crop&auto=format`)}
        alt={book.title}
        loading='lazy'
      />
      <article id='book-detail-content__article'>
        <Typography variant='h4'>{book.title}</Typography>
        <Typography component='p'>
          Authors:&nbsp;
          {book.authorNames.join(', ')}
        </Typography>
        <Typography component='p'>Publisher: {book.publisher}</Typography>
        <Typography component='p'>ISBN: {book.isbn}</Typography>
        <Typography component='p'>Year: {book.year}</Typography>
        <Typography component='p'>
          Genres:&nbsp;
          {book.genreNames.join(', ')}
        </Typography>
        <Typography component='p'>Description: {book.description}</Typography>
      </article>
    </div>
  );
};

export default BookDetail;
