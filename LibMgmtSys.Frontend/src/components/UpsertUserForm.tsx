import { Box, Button, Modal, TextField, Typography } from '@mui/material';
import useAppDispatch from '../hooks/useAppDispatch';
import { putUser, registerUser } from '../store/reducers/userReducer';
import useInputHook from '../hooks/useInputHook';
import { useState } from 'react';
import { User } from '../interfaces/User';

const UpsertUserForm = ({
  formTitle,
  user,
  open,
  onClose,
  onFormSubmit,
}: {
  formTitle: string;
  user?: User;
  open: boolean;
  onClose: () => void;
  onFormSubmit: () => void;
}) => {
  const dispatch = useAppDispatch();
  const firstName = useInputHook(user ? user.firstName : '');
  const lastName = useInputHook(user ? user.lastName : '');
  const email = useInputHook(user ? user.email : '');
  const password = useInputHook('');
  const confirmedPassword = useInputHook('');
  const [passwordError, setPasswordError] = useState<string>('');

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (formTitle === 'Register') {
      await dispatch(
        registerUser({
          firstName: firstName.value as string,
          lastName: lastName.value as string,
          email: email.value as string,
          password: password.value as string,
        })
      );
    }

    if (formTitle === 'Edit Profile') {
      await dispatch(
        putUser({
          firstName: firstName.value as string,
          lastName: lastName.value as string,
          email: email.value as string,
          password: password.value as string,
        })
      );
    }
    onClose();
    onFormSubmit();
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
          {formTitle}
        </Typography>
        <TextField
          className='form__input'
          label='First Name'
          value={firstName.value}
          variant='outlined'
          required={formTitle === 'Register'}
          onChange={firstName.onChange}
        />
        <TextField
          className='form__input'
          label='Last Name'
          value={lastName.value}
          variant='outlined'
          required={formTitle === 'Register'}
          onChange={lastName.onChange}
        />
        <TextField
          className='form__input'
          label='Email'
          value={email.value}
          type='email'
          variant='outlined'
          required={formTitle === 'Register'}
          onChange={email.onChange}
        />
        <TextField
          className='form__input'
          label='Password'
          type='password'
          variant='outlined'
          required={formTitle === 'Register'}
          onChange={password.onChange}
        />
        <TextField
          className='form__input'
          label='Confirmed Password'
          type='password'
          variant='outlined'
          required={formTitle === 'Register'}
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
            {formTitle.split(' ')[0]}
          </Button>
        </Box>
      </Box>
    </Modal>
  );
};

export default UpsertUserForm;
