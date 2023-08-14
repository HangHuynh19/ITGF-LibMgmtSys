import { AppBar, Box, Button, Toolbar } from '@mui/material';
import LoginForm from './LoginForm';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Header = () => {
  const navigate = useNavigate();
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);

  const onLogoClick = () => {
    navigate('/');
  };

  const handleOpeningLoginModal = () => {
    setIsLoginModalOpen(true);
  };
  const handleClosingLoginModal = () => {
    setIsLoginModalOpen(false);
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
          <Button className='app-header__btn' color='primary'>
            Register
          </Button>
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Header;
