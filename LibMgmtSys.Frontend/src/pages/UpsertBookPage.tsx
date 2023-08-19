import { useEffect, useState } from 'react';
import UpsertBookForm from '../components/UpsertBookForm';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { Typography } from '@mui/material';
import { fetchAllAuthors } from '../store/reducers/authorReducer';
import {fetchAllGenres} from '../store/reducers/genreReducer';

const UpsertBookPage = () => {
  const authors = useAppSelector((state) => state.authorReducer.authors);
  const genres = useAppSelector((state) => state.genreReducer.genres);
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    dispatch(fetchAllAuthors());
    dispatch(fetchAllGenres());
    setLoading(false);
  }, [dispatch]);

  if (loading) {
    return <Typography variant='h6'>Loading...</Typography>;
  }

  return <UpsertBookForm formTitle='Add Book' authors={authors} genres={genres} />;
};

export default UpsertBookPage;
