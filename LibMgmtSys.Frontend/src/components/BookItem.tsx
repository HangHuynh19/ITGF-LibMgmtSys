import { IconButton, ImageListItem, ImageListItemBar } from '@mui/material';
import { Book } from '../interfaces/Book';
import { Link } from 'react-router-dom';
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';
import useAppDispatch from '../hooks/useAppDispatch';
import { addToCart, removeFromCart } from '../store/reducers/cartReducer';
import DeleteIcon from '@mui/icons-material/Delete';
import useAppSelector from '../hooks/useAppSelector';

const BookItem = ({
  book,
  showAddToCart,
}: {
  book: Book;
  showAddToCart: boolean;
}) => {
  const dispatch = useAppDispatch();
  const isLoggedIn = useAppSelector((state) => state.authReducer.isLoggedIn);
  const authorString = book.authorNames.join(', ');
  const randomInt = Math.floor(Math.random() * 15) + 1;

  const handleAddToCart = () => {
    dispatch(addToCart(book.id));
  };

  const handleDeleteFromCart = () => {
    dispatch(removeFromCart(book.id));
  };

  return (
    <>
      <ImageListItem style={{ maxHeight: '492px' }}>
        <Link to={`/books/${book.id}`}>
          <ImageListItem>
            <img
              src={require(`../assets/bookImages/book${randomInt}.jpeg?w=164&h=164&fit=crop&auto=format`)}
              alt={book.title}
              loading='lazy'
              style={{ width: '100%', height: '492px' }}
            />
          </ImageListItem>
        </Link>
        <ImageListItemBar
          title={book.title}
          subtitle={authorString}
          actionIcon={
            isLoggedIn ? (
              showAddToCart ? (
                <IconButton style={{ paddingRight: '0.5em' }}>
                  <AddShoppingCartIcon
                    sx={{ color: 'white' }}
                    onClick={handleAddToCart}
                  />
                </IconButton>
              ) : (
                <IconButton style={{ paddingRight: '0.5em' }}>
                  <DeleteIcon
                    sx={{ color: 'white' }}
                    onClick={handleDeleteFromCart}
                  />
                </IconButton>
              )
            ) : null
          }
        />
      </ImageListItem>
    </>
  );
};

export default BookItem;
