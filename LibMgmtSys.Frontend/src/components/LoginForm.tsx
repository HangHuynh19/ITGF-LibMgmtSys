import { Box, Button, Modal, TextField, Typography } from '@mui/material';
import useAppDispatch from '../hooks/useAppDispatch';
import { authenticate } from '../store/reducers/authReducer';
import useInputHook from '../hooks/useInputHook';
import { fetchCustomerProfile } from '../store/reducers/customerReducer';
import { checkIsAmin } from '../store/reducers/userReducer';
import {fetchCustomerLoans} from '../store/reducers/loanReducer';

const LoginForm = ({
  open,
  onClose,
}: {
  open: boolean;
  onClose: () => void;
}) => {
  const dispatch = useAppDispatch();
  const email = useInputHook('');
  const password = useInputHook('');

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    await dispatch(
      authenticate({ email: email.value as string, password: password.value as string }),
    );
    await dispatch(fetchCustomerProfile());
    await dispatch(checkIsAmin()); 
    await dispatch(fetchCustomerLoans());
    onClose();
  };

  const handleCancel = () => {
    email.reset();
    password.reset();
    onClose();
  };

  return (
    <>
      <Modal open={open} onClose={onClose}>
        <Box className='form' component='form' onSubmit={handleSubmit}>
          <Typography className='form__title' variant='h5'>
            Login
          </Typography>
          <TextField
            className='form__input'
            label='Email'
            type='email'
            variant='outlined'
            color='secondary'
            required
            onChange={email.onChange}
          />
          <TextField
            className='form__input'
            label='Password'
            type='password'
            variant='outlined'
            color='secondary'
            required
            onChange={password.onChange}
          />
          <Box className='form__btn-group'>
            <Button
              className='form__cancel-btn'
              variant='contained'
              type='button'
              onClick={handleCancel}
            >
              Cancel
            </Button>
            <Button
              className='form__agree-btn'
              variant='contained'
              type='submit'
            >
              Login
            </Button>
          </Box>
        </Box>
      </Modal>
    </>
  );
};

export default LoginForm;
