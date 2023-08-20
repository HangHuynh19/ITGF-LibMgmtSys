import { Box, Button, Modal, Typography } from '@mui/material';
import { Book } from '../interfaces/Book';
import useAppDispatch from '../hooks/useAppDispatch';
import { removeBook } from '../store/reducers/bookReducer';
import { useNavigate } from 'react-router-dom';

const DeleteBookDialog = ({
  book,
  isOpen,
  onClose,
}: {
  book: Book;
  isOpen: boolean;
  onClose: () => void;
}) => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleDelete = () => {
    dispatch(removeBook(book.id));
    onClose();
    navigate('/');
  };

  const handleCancel = () => {
    onClose();
  };

  return (
    <Modal open={isOpen} onClose={onClose}>
      <Box className='form'>
        <Typography variant='body1'>
          Are you sure you want to delete <i>{book.title}</i>?
        </Typography>
        <div id='delete-book-dialog__btn-group' className='form__btn-group'>
          <Button
            className='form__agree-btn'
            variant='contained'
            onClick={handleDelete}
          >
            Delete
          </Button>
          <Button
            className='form__cancel-btn'
            variant='contained'
            onClick={handleCancel}
          >
            Cancel
          </Button>
        </div>
      </Box>
    </Modal>
  );
};

export default DeleteBookDialog;
