import {
  Box,
  Button,
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  FormLabel,
  TextField,
  Typography,
} from '@mui/material';
import { Book, UpsertBook } from '../interfaces/Book';
import useInputHook from '../hooks/useInputHook';
import { useState } from 'react';
import { Author } from '../interfaces/Author';
import { Set } from 'typescript';
import { Genre } from '../interfaces/Genre';
import useAppDispatch from '../hooks/useAppDispatch';
import { postBook } from '../store/reducers/bookReducer';
import { useNavigate } from 'react-router-dom';

const CreateBookForm = ({
  authors,
  genres,
}: {
  authors: Author[];
  genres: Genre[];
}) => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const title = useInputHook('');
  const isbn = useInputHook('');
  const publisher = useInputHook('');
  const year = useInputHook(0);
  const description = useInputHook('');
  const borrowingPeriod = useInputHook(0);
  const quantity = useInputHook(0);
  const [authorIds, setAuthorIds] = useState<Set<string>>(new Set<string>());
  const [genreIds, setGenreIds] = useState<Set<string>>(new Set<string>());

  const handleAuthorToggle = (authorId: string) => {
    const newAuthorIds = new Set<string>();
    authorIds.forEach((id) => newAuthorIds.add(id));
    if (newAuthorIds.has(authorId)) {
      newAuthorIds.delete(authorId);
    } else {
      newAuthorIds.add(authorId);
    }
    setAuthorIds(newAuthorIds);
  };

  const handleGenreToggle = (genreId: string) => {
    const newGenreIds = new Set<string>();
    genreIds.forEach((id) => newGenreIds.add(id));
    if (newGenreIds.has(genreId)) {
      newGenreIds.delete(genreId);
    } else {
      newGenreIds.add(genreId);
    }
    setGenreIds(newGenreIds);
  };

  const setToArray = (set: Set<string>) => {
    const array: string[] = [];
    set.forEach((item) => array.push(item));
    return array;
  };

  const handleCancel = () => {
    /* title.reset();
    isbn.reset();
    publisher.reset();
    year.reset();
    description.reset();
    borrowingPeriod.reset();
    quantity.reset();
    setAuthorIds(new Set<string>());
    setGenreIds(new Set<string>()); */
    navigate('/');
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const authorIdsArray = setToArray(authorIds);
    const genreIdsArray = setToArray(genreIds);
    const input: UpsertBook = {
      title: title.value as string,
      isbn: isbn.value as string,
      publisher: publisher.value as string,
      authorIds: authorIdsArray,
      year: year.value as number,
      description: description.value as string,
      genreIds: genreIdsArray,
      borrowingPeriod: borrowingPeriod.value as number,
      quantity: quantity.value as number,
    };
    console.log('createBook input', input);
    dispatch(postBook(input));

    navigate('/');
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

        <FormControl variant='outlined'>
          <FormLabel>Authors</FormLabel>
          <FormGroup row>
            {authors.map((author) => (
              <FormControlLabel
                key={author.id}
                control={
                  <Checkbox
                    checked={authorIds.has(author.id)}
                    onChange={() => handleAuthorToggle(author.id)}
                  />
                }
                label={author.name}
              />
            ))}
          </FormGroup>
        </FormControl>
        <FormControl variant='outlined'>
          <FormLabel>Genres</FormLabel>
          <FormGroup row>
            {genres.map((genre) => (
              <FormControlLabel
                key={genre.id}
                control={
                  <Checkbox
                    checked={genreIds.has(genre.id)}
                    onChange={() => handleGenreToggle(genre.id)}
                  />
                }
                label={genre.name}
              />
            ))}
          </FormGroup>
        </FormControl>
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

export default CreateBookForm;
