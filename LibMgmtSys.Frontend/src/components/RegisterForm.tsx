import { Box, Button, Modal, TextField, Typography } from '@mui/material';
import useAppDispatch from '../hooks/useAppDispatch';
import { registerUser } from '../store/reducers/userReducer';
import useInputHook from '../hooks/useInputHook';
import useAppSelector from '../hooks/useAppSelector';
import { useState } from 'react';

const RegisterForm = ({
  open,
  onClose,
}: {
  open: boolean;
  onClose: () => void;
}) => {
  const dispatch = useAppDispatch();
  const user = useAppSelector((state) => state.userReducer.user);
  const firstName = useInputHook(user ? user.firstName : '');
  const lastName = useInputHook(user ? user.lastName : '');
  const email = useInputHook(user ? user.email : '');
  const password = useInputHook('');
  const confirmedPassword = useInputHook('');
  const [passwordError, setPasswordError] = useState<string>('');

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    await dispatch(
      registerUser({
        firstName: firstName.value as string,
        lastName: lastName.value as string,
        email: email.value as string, 
        password: password.value as string,
      })
    );
    onClose();
  };

  const handleCancel = () => {
    firstName.reset();
    lastName.reset();
    email.reset();
    password.reset();
    onClose();
  };

  const handleConfirmedPasswordChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    confirmedPassword.onChange(e);
    if (password.value !== e.target.value) {
      setPasswordError('Passwords do not match');
    } else {
      setPasswordError('');
    }
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box className='form' component='form' onSubmit={handleSubmit}>
        <Typography className='form__title' variant='h5'>
          Register
        </Typography>
        <TextField
          className='form__input'
          label='First Name'
          variant='outlined'
          required
          onChange={firstName.onChange}
        />
        <TextField
          className='form__input'
          label='Last Name'
          variant='outlined'
          required
          onChange={lastName.onChange}
        />
        <TextField
          className='form__input'
          label='Email'
          type='email'
          variant='outlined'
          required
          onChange={email.onChange}
        />
        <TextField
          className='form__input'
          label='Password'
          type='password'
          variant='outlined'
          required
          onChange={password.onChange}
        />
        <TextField
          className='form__input'
          label='Confirmed Password'
          type='password'
          variant='outlined'
          required
          error={!!passwordError}
          helperText={passwordError ? `${passwordError}` : ''}
          onChange={handleConfirmedPasswordChange}
        />
        <Box className='form__btn-group'>
          <Button
            className='form__cancel-btn'
            variant='contained'
            onClick={handleCancel}
          >
            Cancel
          </Button>
          <Button className='form__agree-btn' variant='contained' type='submit'>
            Register
          </Button>
        </Box>
      </Box>
    </Modal>
  );
};

export default RegisterForm;
