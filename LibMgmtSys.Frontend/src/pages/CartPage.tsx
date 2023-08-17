import { useEffect } from 'react';
import useAppDispatch from '../hooks/useAppDispatch';
import useAppSelector from '../hooks/useAppSelector';
import { Typography } from '@mui/material';
import { filterBooks } from '../store/reducers/bookReducer';

const CartPage = () => {
  const cart = useAppSelector((state) => state.cartReducer.cart);
  const books = useAppSelector((state) => state.bookReducer);
  const dispatch = useAppDispatch();

  useEffect(() => {
    const fetchAllBooksInCart = () => {
      dispatch(filterBooks({ ids: cart }));
    };
    fetchAllBooksInCart();
  }, [cart, dispatch]);

  return (
    <div>
      <h1>Cart Page</h1>
      {books ? (
        books.books.map((book) => (
          <div key={book.id}>
            <Typography variant='h6'>{book.title}</Typography>
            <Typography component='p'>{book.authorNames}</Typography>
          </div>
        ))
      ) : (
        <Typography variant='h6' align='center'>
          No books in cart!
        </Typography>
      )}
    </div>
  );
};

export default CartPage;
