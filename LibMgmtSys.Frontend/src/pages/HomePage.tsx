import { useEffect, useState } from 'react';
import BookList from '../components/BookList';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { fetchAllBooks } from '../store/reducers/bookReducer';
import { Button, Typography } from '@mui/material';
import SearchBar from '../components/SearchBar';
import SortingIconButton from '../components/SortingIconButton';
import { fetchCustomerProfile } from '../store/reducers/customerReducer';
import { checkIsAmin } from '../store/reducers/userReducer';
import { fetchCustomerLoans } from '../store/reducers/loanReducer';
import { Link } from 'react-router-dom';

const HomePage = () => {
  const books = useAppSelector((state) => state.bookReducer.books);
  const isAdmin = useAppSelector((state) => state.userReducer.isAdmin);
  const dispatch = useAppDispatch();
  const [searchTerm, setSearchTerm] = useState('');
  const [sortingOrder, setSortingOrder] = useState('asc');

  const handleSearchTermSent = (searchTerm: string) => {
    setSearchTerm(searchTerm);
  };

  const handleSortingOrderSent = (sortingOrder: string) => {
    setSortingOrder(sortingOrder);
  };

  useEffect(() => {
    dispatch(
      fetchAllBooks({
        sortingOrder: sortingOrder,
        searchTerm: searchTerm,
      })
    );
    dispatch(checkIsAmin());
    dispatch(fetchCustomerProfile());
    dispatch(fetchCustomerLoans());
  }, [dispatch, searchTerm, sortingOrder]);

  return (
    <div className='home-page-container'>
      <div className='home-page__search-and-sort'>
        <div className='home-page__search-and-sort__btn-group'>
          <SortingIconButton onSortingOrderSent={handleSortingOrderSent} />
          <SearchBar onSearchTermSent={handleSearchTermSent} />
        </div>
        {isAdmin && (
          <Link to='/books/add-book'>
            <Button
              className='home-page-container__create-book-btn'
              variant='contained'
              color='secondary'
            >
              Add Book
            </Button>
          </Link>
        )}
      </div>
      {books ? (
        <BookList
          books={books}
          sortingOrder={sortingOrder}
          searchTerm={searchTerm}
        />
      ) : (
        <Typography variant='h6'>No books found</Typography>
      )}
      ;
    </div>
  );
};

export default HomePage;
