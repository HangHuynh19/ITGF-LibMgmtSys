import UpdateBookForm from '../components/UpdateBookForm';
import { useEffect } from 'react';
import { fetchAllBooks } from '../store/reducers/bookReducer';
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
