import { useParams } from 'react-router-dom';
import useAppSelector from '../hooks/useAppSelector';
import UpdateBookForm from '../components/UpdateBookForm';
import { Typography } from '@mui/material';
import { useEffect } from 'react';
import { fetchAllBooks, filterBooks } from '../store/reducers/bookReducer';
import useAppDispatch from '../hooks/useAppDispatch';

const UpdateBookPage = () => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(
      fetchAllBooks({
        sortingOrder: 'asc',
        searchTerm: '',
      })
    );
  }, [dispatch]);

  return (
    <>
      <UpdateBookForm />
    </>
  );
};

export default UpdateBookPage;
