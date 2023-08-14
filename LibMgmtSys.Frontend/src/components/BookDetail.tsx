import { Book } from '../interfaces/Book';
import { ImageListItem, Typography } from '@mui/material';

const BookDetail = ({ book }: { book: Book }) => {
  const randomInt = Math.floor(Math.random() * 15) + 1;

  return (
    <>
      <ImageListItem>
        <img
          src={require(`../assets/book${randomInt}.jpeg?w=248&h=248&fit=crop&auto=format`)}
          alt={book.title}
          loading='lazy'
        />
      </ImageListItem>
      <article>
        <Typography variant='h4' align='center'>
          {book.title}
        </Typography>
        <Typography component='p' align='center'>
          Authors:&nbsp;
          {book.authorNames.join(', ')}
        </Typography>
        <Typography component='p' align='center'>
          Publisher: {book.publisher}
        </Typography>
        <Typography component='p' align='center'>
          Year: {book.year}
        </Typography>
        <Typography component='p' align='center'>
          Genres:&nbsp;
          {book.genreNames.join(', ')}
        </Typography>
        <Typography component='p' align='center'>
          Description: {book.description}
        </Typography>
      </article>
    </>
  );
};

export default BookDetail;
