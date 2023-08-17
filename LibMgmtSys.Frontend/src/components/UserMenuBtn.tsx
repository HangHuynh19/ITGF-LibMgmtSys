import { Box, Menu, MenuItem, Typography } from '@mui/material';
import { useState } from 'react';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Link } from 'react-router-dom';
import useAppDispatch from '../hooks/useAppDispatch';
import { clearUser } from '../store/reducers/userReducer';
import { clearCustomer } from '../store/reducers/customerReducer';
import { logout } from '../store/reducers/authReducer';
import { clearCart } from '../store/reducers/cartReducer';

const UserMenuBtn = () => {
  const dispatch = useAppDispatch();
  const [anchorElUser, setAnchorElUser] = useState<null | HTMLElement>(null);

  const handleOpenUserMenu = (event: React.MouseEvent<HTMLDivElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleLogout = () => {
    dispatch(clearUser());
    dispatch(clearCustomer());
    dispatch(logout());
    dispatch(clearCart());
    handleCloseUserMenu();
  };

  return (
    <Box>
      <div onClick={handleOpenUserMenu}>
        <AccountCircleIcon fontSize='large' color='primary' />
      </div>
      <Menu
        anchorEl={anchorElUser}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'right',
        }}
        keepMounted
        transformOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        open={Boolean(anchorElUser)}
        onClose={handleCloseUserMenu}
      >
        <MenuItem
          component={Link}
          to='/customers/profile'
          onClick={handleCloseUserMenu}
        >
          <Typography textAlign='center'>Profile</Typography>
        </MenuItem>
        <MenuItem component={Link} to='/' onClick={handleLogout}>
          <Typography textAlign='center'>Logout</Typography>
        </MenuItem>
      </Menu>
    </Box>
  );
};

export default UserMenuBtn;
