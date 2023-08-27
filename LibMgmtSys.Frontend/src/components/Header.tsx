import {
  AppBar,
  Box,
  Button,
  IconButton,
  Toolbar
} from '@mui/material';
import LoginForm from './LoginForm';
import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import useAppSelector from '../hooks/useAppSelector';
import UserMenuBtn from './UserMenuBtn';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import UpsertUserForm from './UpsertUserForm';
import useAppDispatch from '../hooks/useAppDispatch';
import { fetchCustomerProfile } from '../store/reducers/customerReducer';

const Header = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const isLoggedIn = useAppSelector((state) => state.authReducer.isLoggedIn);
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
  const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);

  const onLogoClick = () => {
    navigate('/');
  };

  const handleOpeningLoginModal = () => {
    setIsLoginModalOpen(true);
  };

  const handleClosingLoginModal = () => {
    setIsLoginModalOpen(false);
  };

  const handleOpeningRegisterModal = () => {
    setIsRegisterModalOpen(true);
  };

  const handleClosingRegisterModal = () => {
    setIsRegisterModalOpen(false);
  };

  const handleRegisterFormSubmit = () => {
    dispatch(fetchCustomerProfile());
  };

  return (
    <Box className='app-header' sx={{ flexGrow: 1 }}>
      <AppBar position='static' color='secondary'>
        <Toolbar>
          <div style={{ flexGrow: 1 }}>
            <img
              src={require('../assets/logo.png')}
              alt='logo'
              onClick={onLogoClick}
              style={{ width: '7em', height: '7em' }}
            />
          </div>
          {isLoggedIn ? (
            <>
              <UserMenuBtn />
              <Link className='app-header__icon' to='/cart'>
                <IconButton color='primary'>
                  <ShoppingCartIcon />
                </IconButton>
              </Link>
            </>
          ) : (
            <>
              <Button
                className='app-header__btn'
                color='primary'
                onClick={handleOpeningLoginModal}
              >
                Login
              </Button>
              <LoginForm
                open={isLoginModalOpen}
                onClose={handleClosingLoginModal}
              />
              <Button
                className='app-header__btn'
                color='primary'
                onClick={handleOpeningRegisterModal}
              >
                Register
              </Button>
              <UpsertUserForm
                formTitle='Register'
                open={isRegisterModalOpen}
                onClose={handleClosingRegisterModal}
                onFormSubmit={handleRegisterFormSubmit}
              />
            </>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Header;
