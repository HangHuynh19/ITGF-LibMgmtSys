import { IconButton, ImageListItem, ImageListItemBar } from '@mui/material';
import { Book } from '../interfaces/Book';
import { Link } from 'react-router-dom';
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';
import useAppDispatch from '../hooks/useAppDispatch';
import { addToCart } from '../store/reducers/cartReducer';

const BookItem = ({ book }: { book: Book }) => {
  const dispatch = useAppDispatch();
  const authorString = book.authorNames.join(', ');
  const randomInt = Math.floor(Math.random() * 15) + 1;

  const handleAddToCart = () => {
    dispatch(addToCart(book.id));
  };

  return (
    <>
      <ImageListItem>
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
            <IconButton style={{ paddingRight: '0.5em' }}>
              <AddShoppingCartIcon
                sx={{ color: 'white' }}
                onClick={handleAddToCart}
              />
            </IconButton>
          }
        />
      </ImageListItem>
    </>
  );
};

export default BookItem;
