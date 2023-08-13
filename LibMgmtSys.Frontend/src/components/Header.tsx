import { AppBar, Box, Button, Toolbar } from '@mui/material';
const Header = () => {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position='static' color='secondary'>
        <Toolbar>
          <div style={{ flexGrow: 1 }}>
            <img
              src={require('../assets/logo.png')}
              alt='logo'
              style={{ width: '7em', height: '7em' }}
            />
          </div>
          <Button color='primary'>
            Login
          </Button>
          <Button color='primary'>
            Register
          </Button>
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Header;
