import { useEffect } from 'react';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { Button, ImageList, Typography } from '@mui/material';
import { filterBooks } from '../store/reducers/bookReducer';
import BookItem from '../components/BookItem';
import { createLoansFromBookIds } from '../api/apiCalls';
import { useNavigate } from 'react-router-dom';
import { clearCart } from '../store/reducers/cartReducer';

const CartPage = () => {
  const navigate = useNavigate();
  const cart = useAppSelector((state) => state.cartReducer.cart);
  const token = localStorage.getItem('token');
  const books = useAppSelector((state) => state.bookReducer);
  const dispatch = useAppDispatch();

  const handleReserveBtnClicked = () => {
    if (token) {
      createLoansFromBookIds(cart, token);
    }
    dispatch(clearCart());
    navigate('/');
  };

  useEffect(() => {
    const fetchAllBooksInCart = () => {
      dispatch(filterBooks({ ids: cart }));
    };
    fetchAllBooksInCart();
  }, [cart, dispatch]);

  return (
    <div className='reservation-list'>
      <div className='reservation-list__header'>
        <Typography className='reservation-list__text' variant='h4'>
          Book Reservation List
        </Typography>
        <Button
          className='reservation-list__text'
          color='primary'
          variant='contained'
          onClick={handleReserveBtnClicked}
        >
          Reserve
        </Button>
      </div>
      <ImageList cols={4} sx={{ width: '100%', height: '100%' }}>
        {books ? (
          books.books.map((book) => (
            <BookItem key={book.id} book={book} showAddToCart={false} />
          ))
        ) : (
          <Typography variant='h6' align='center'>
            No books in cart!
          </Typography>
        )}
      </ImageList>
    </div>
  );
};

export default CartPage;
