import { Button, IconButton, Typography } from '@mui/material';
import useAppSelector from '../hooks/useAppSelector';
import MailOutlineIcon from '@mui/icons-material/MailOutline';
import BorderColorIcon from '@mui/icons-material/BorderColor';

const ProfilePage = () => {
  const customer = useAppSelector((state) => state.customerReducer.customer);
  const randomInt = Math.floor(Math.random() * 8) + 1;

  return (
    <>
      {customer ? (
        <div className='profile'>
          <div className='profile__avatar-container'>
            <img
              className='profile__avatar-container__img'
              src={require(`../assets/profileImages/cat${randomInt}.jpeg?w=164&h=164&fit=crop&auto=format`)}
              alt='profile'
              loading='lazy'
            />
          </div>
          <article className='profile__article'>
            <div className='profile__info-container'>
              <Typography variant='h4'>
                {customer.firstName} {customer.lastName}
              </Typography>
              <IconButton
                aria-label='edit'
                onClick={() => {}}
                color='primary'
                size='large'
              >
                <BorderColorIcon />
              </IconButton>
            </div>
            <div className='profile__info-container'>
              <MailOutlineIcon color='primary' />
              <Typography component={'p'}>{customer.email}</Typography>
            </div>
          </article>
        </div>
      ) : (
        <Typography variant='h4'>Customer not found</Typography>
      )}
    </>
  );
};

export default ProfilePage;
