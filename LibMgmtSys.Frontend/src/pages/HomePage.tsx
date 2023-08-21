import { useEffect, useState } from 'react';
import BookList from '../components/BookList';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { fetchAllBooks } from '../store/reducers/bookReducer';
import { Button } from '@mui/material';
import SearchBar from '../components/SearchBar';
import SortingIconButton from '../components/SortingIconButton';

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
  }, [dispatch, searchTerm, sortingOrder]);

  console.log('SearchTerm in HomePage', searchTerm);
  console.log('SortingOrder in HomePage', sortingOrder);

  return (
    <div className='home-page-container'>
      <div className='home-page__search-and-sort'>
        <div className='home-page__search-and-sort__btn-group'>
          <SortingIconButton onSortingOrderSent={handleSortingOrderSent} />
          <SearchBar onSearchTermSent={handleSearchTermSent} />
        </div>
        {isAdmin && (
          <Button
            className='home-page-container__create-book-btn'
            variant='contained'
            color='secondary'
            href='/books/add-book'
          >
            Add Book
          </Button>
        )}
      </div>
      <BookList
        books={books}
        sortingOrder={sortingOrder}
        searchTerm={searchTerm}
      />
      ;
    </div>
  );
};

export default HomePage;
