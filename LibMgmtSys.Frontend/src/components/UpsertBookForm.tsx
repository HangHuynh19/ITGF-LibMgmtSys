import {
  Box,
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  FormLabel,
  TextField,
  Typography,
} from '@mui/material';
import { Book } from '../interfaces/Book';
import useInputHook from '../hooks/useInputHook';
import { useState } from 'react';
import { Author } from '../interfaces/Author';
import { Set } from 'typescript';
import { Genre } from '../interfaces/Genre';

const UpsertBookForm = ({
  formTitle,
  book,
  authors,
  genres,
}: {
  formTitle: string;
  book?: Book;
  authors: Author[];
  genres: Genre[];
}) => {
  const title = useInputHook(book?.title || '');
  const isbn = useInputHook(book?.isbn || '');
  const publisher = useInputHook(book?.publisher || '');
  const year = useInputHook(book?.year || 0);
  const description = useInputHook(book?.description || '');
  const borrowingPeriod = useInputHook(book?.borrowingPeriod || 0);
  const quantity = useInputHook(book?.quantity || 0);
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

  console.log('genreIds', genreIds);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {};

  return (
    <>
      <Box className='page-container' component='form' onSubmit={handleSubmit}>
        <Typography className='page-container__title' variant='h5'>
          {formTitle}
        </Typography>
        <TextField
          className='.page-container__input-single'
          label='Book Title'
          value={title.value}
          variant='outlined'
          required={formTitle === 'Add Book'}
          onChange={title.onChange}
        />
        <TextField
          className='.page-container__input-single'
          label='Description'
          multiline
          maxRows={4}
          value={description.value}
          variant='outlined'
          required={formTitle === 'Add Book'}
          onChange={description.onChange}
        />
        {formTitle === 'Add Book' && (
          <>
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
          </>
        )}
        <div className='page-container__input-group'>
          <TextField
            className='page-container__input-group__input'
            label='ISBN'
            value={isbn.value}
            variant='outlined'
            required={formTitle === 'Add Book'}
            onChange={isbn.onChange}
          />
          <TextField
            className='page-container__input-group__input'
            label='Publisher'
            value={publisher.value}
            variant='outlined'
            required={formTitle === 'Add Book'}
            onChange={publisher.onChange}
          />
        </div>
        <div className='page-container__input-group'>
          <TextField
            className='page-container__input-group__input'
            label='Year'
            value={year.value}
            variant='outlined'
            required={formTitle === 'Add Book'}
            onChange={year.onChange}
          />
          <TextField
            className='page-container__input-group__input'
            label='Borrowing Period'
            value={borrowingPeriod.value}
            variant='outlined'
            required={formTitle === 'Add Book'}
            onChange={borrowingPeriod.onChange}
          />
          <TextField
            className='page-container__input-group__input'
            label='Quantity'
            value={quantity.value}
            variant='outlined'
            required={formTitle === 'Add Book'}
            onChange={quantity.onChange}
          />
        </div>
      </Box>
    </>
  );
};

export default UpsertBookForm;
