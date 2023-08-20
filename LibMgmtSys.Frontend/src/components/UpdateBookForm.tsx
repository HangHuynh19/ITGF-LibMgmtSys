import { useNavigate, useParams } from 'react-router-dom';
import useAppDispatch from '../hooks/useAppDispatch';
import { UpsertBook } from '../interfaces/Book';
import useInputHook from '../hooks/useInputHook';
import { putBook } from '../store/reducers/bookReducer';
import { Box, Button, TextField, Typography } from '@mui/material';
import useAppSelector from '../hooks/useAppSelector';

const UpdateBookForm = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const book = useAppSelector((state) =>
    state.bookReducer.books.find((book) => book.id === id)
  );

  const title = useInputHook(book?.title || '');
  const isbn = useInputHook(book?.isbn || '');
  const publisher = useInputHook(book?.publisher || '');
  const year = useInputHook(book?.year || 0);
  const description = useInputHook(book?.description || '');
  const borrowingPeriod = useInputHook(book?.borrowingPeriod || 0);
  const quantity = useInputHook(book?.quantity || 0);

  const handleCancel = () => {
    title.reset();
    isbn.reset();
    publisher.reset();
    year.reset();
    description.reset();
    borrowingPeriod.reset();
    quantity.reset();
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const input: UpsertBook = {
      title: title.value as string,
      isbn: isbn.value as string,
      publisher: publisher.value as string,
      year: year.value as number,
      description: description.value as string,
      image: book?.image || 'https://i.imgur.com/1qk9n0z.pn',
      borrowingPeriod: borrowingPeriod.value as number,
      quantity: quantity.value as number,
    };
    console.log('updateBook input', input);

    if (!book) {
      return;
    }

    dispatch(
      putBook({
        id: book.id,
        bookToUpdate: input,
      })
    );

    navigate(`/books/${book.id}`);
  };

  return (
    <>
      <Box className='page-container' component='form' onSubmit={handleSubmit}>
        <Typography className='page-container__title' variant='h5'>
          Add Book
        </Typography>
        <TextField
          className='.page-container__input-single'
          label='Book Title'
          value={title.value}
          variant='outlined'
          required
          onChange={title.onChange}
        />
        <TextField
          className='.page-container__input-single'
          label='Description'
          multiline
          maxRows={4}
          value={description.value}
          variant='outlined'
          required
          onChange={description.onChange}
        />
        <div className='page-container__input-group'>
          <TextField
            className='page-container__input-group__input'
            label='ISBN'
            value={isbn.value}
            variant='outlined'
            required
            onChange={isbn.onChange}
          />
          <TextField
            className='page-container__input-group__input'
            label='Publisher'
            value={publisher.value}
            variant='outlined'
            required
            onChange={publisher.onChange}
          />
        </div>
        <div className='page-container__input-group'>
          <TextField
            className='page-container__input-group__input'
            label='Year'
            value={year.value}
            variant='outlined'
            required
            onChange={year.onChange}
          />
          <TextField
            className='page-container__input-group__input'
            label='Borrowing Period'
            value={borrowingPeriod.value}
            variant='outlined'
            required
            onChange={borrowingPeriod.onChange}
          />
          <TextField
            className='page-container__input-group__input'
            label='Quantity'
            value={quantity.value}
            variant='outlined'
            required
            onChange={quantity.onChange}
          />
        </div>
        <div className='page-container__input-group__btn-group'>
          <Button
            className='input-group__btn-group__cancel-btn'
            variant='contained'
            onClick={handleCancel}
          >
            Cancel
          </Button>
          <Button
            className='input-group__btn-group__agree-btn'
            variant='contained'
            type='submit'
          >
            Submit
          </Button>
        </div>
      </Box>
    </>
  );
};

export default UpdateBookForm;
