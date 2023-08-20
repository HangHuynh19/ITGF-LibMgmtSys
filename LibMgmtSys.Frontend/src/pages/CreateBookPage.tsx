import { useEffect, useState } from 'react';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { Typography } from '@mui/material';
import { fetchAllAuthors } from '../store/reducers/authorReducer';
import {fetchAllGenres} from '../store/reducers/genreReducer';
import CreateBookForm from '../components/CreateBookForm';

const CreateBookPage = () => {
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

  return <CreateBookForm authors={authors} genres={genres} />;
};

export default CreateBookPage;
