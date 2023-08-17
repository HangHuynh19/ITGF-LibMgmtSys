import { AppBar, Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import LoginForm from './LoginForm';
import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import useAppSelector from '../hooks/useAppSelector';
import RegisterForm from './RegisterForm';
import UserMenuBtn from './UserMenuBtn';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

const Header = () => {
  const navigate = useNavigate();
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
              <Link to='/cart'>
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
              <RegisterForm
                open={isRegisterModalOpen}
                onClose={handleClosingRegisterModal}
              />
            </>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Header;
