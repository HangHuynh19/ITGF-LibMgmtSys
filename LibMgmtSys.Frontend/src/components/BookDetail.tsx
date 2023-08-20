import { Book } from '../interfaces/Book';
import { IconButton, Typography } from '@mui/material';
import BorderColorIcon from '@mui/icons-material/BorderColor';
import DeleteIcon from '@mui/icons-material/Delete';
import { Link } from 'react-router-dom';
import { useState } from 'react';
import DeleteBookDialog from './DeleteBookDialog';
import useAppSelector from '../hooks/useAppSelector';

const BookDetail = ({ book }: { book: Book }) => {
  const randomInt = Math.floor(Math.random() * 15) + 1;
  const [isDeleteDialogOpen, setIsDeleteDialogOpen] = useState(false);
  const isAdmin = useAppSelector((state) => state.userReducer.isAdmin);

  const handleOpenDeleteDialog = () => {
    setIsDeleteDialogOpen(true);
  };

  const handleCloseDeleteDialog = () => {
    setIsDeleteDialogOpen(false);
  };

  return (
    <div id='book-detail-content'>
      <img
        id='book-detail-content__img'
        src={require(`../assets/bookImages/book${randomInt}.jpeg?w=164&h=164&fit=crop&auto=format`)}
        alt={book.title}
        loading='lazy'
      />
      <article>
        <div className='book-detail-content__title-section'>
          <Typography className='title-section__title' variant='h4'>
            {book.title}
          </Typography>
          {isAdmin && (
            <div className='title-section__button-group'>
              <Link to={`/books/${book.id}/edit`}>
                <IconButton onClick={handleOpenDeleteDialog}>
                  <BorderColorIcon />
                </IconButton>
              </Link>
              <IconButton onClick={handleOpenDeleteDialog}>
                <DeleteIcon />
              </IconButton>
              <DeleteBookDialog
                book={book}
                isOpen={isDeleteDialogOpen}
                onClose={handleCloseDeleteDialog}
              />
            </div>
          )}
        </div>
        <Typography component='p'>
          <b>Authors:</b>&nbsp;
          {book.authorNames.join(', ')}
        </Typography>
        <Typography component='p'>
          <b>Publisher:</b> {book.publisher}
        </Typography>
        <Typography component='p'>
          <b>ISBN:</b> {book.isbn}
        </Typography>
        <Typography component='p'>
          <b>Year:</b> {book.year}
        </Typography>
        <Typography component='p'>
          <b>Genres:</b>&nbsp;
          {book.genreNames.join(', ')}
        </Typography>
        <Typography component='p'>
          <b>Description:</b> {book.description}
        </Typography>
        <Typography component='p'>
          <b>Borrowing Period:</b> {book.borrowingPeriod}
        </Typography>
        <Typography component='p'>
          <b>Quantity:</b> {book.quantity}
        </Typography>
      </article>
    </div>
  );
};

export default BookDetail;
