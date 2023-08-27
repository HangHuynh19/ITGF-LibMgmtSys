import {IconButton, Typography } from '@mui/material';
import useAppSelector from '../hooks/useAppSelector';
import MailOutlineIcon from '@mui/icons-material/MailOutline';
import BorderColorIcon from '@mui/icons-material/BorderColor';
import { useEffect, useState } from 'react';
import UpsertUserForm from '../components/UpsertUserForm';
import { fetchCustomerProfile } from '../store/reducers/customerReducer';
import useAppDispatch from '../hooks/useAppDispatch';
import { fetchCustomerLoans } from '../store/reducers/loanReducer';
import LoanItem from '../components/LoanItem';

const ProfilePage = () => {
  const dispatch = useAppDispatch();
  const customer = useAppSelector((state) => state.customerReducer.customer);
  const loans = useAppSelector((state) => state.loanReducer.loans);
  const randomInt = Math.floor(Math.random() * 8) + 1;
  const [isEditUserModalOpen, setIsEditUserModalOpen] = useState(false);

  const handleOpenEditUserModal = () => {
    setIsEditUserModalOpen(true);
  };

  const handleCloseEditUserModal = () => {
    setIsEditUserModalOpen(false);
  };

  const handleProfileUpdate = async () => {
    await dispatch(fetchCustomerProfile());
  };

  useEffect(() => {
    dispatch(fetchCustomerLoans());
  }, [dispatch]);

  return (
    <>
      {customer ? (
        <>
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
                  onClick={handleOpenEditUserModal}
                  color='primary'
                  size='large'
                >
                  <BorderColorIcon />
                </IconButton>
                <UpsertUserForm
                  formTitle='Edit Profile'
                  user={customer}
                  open={isEditUserModalOpen}
                  onClose={handleCloseEditUserModal}
                  onFormSubmit={handleProfileUpdate}
                />
              </div>
              <div className='profile__info-container'>
                <MailOutlineIcon color='primary' />
                <Typography component={'p'}>{customer.email}</Typography>
              </div>
            </article>
          </div>
          <div className='loan-container'>
            <Typography className='loan-container__secttion-name' variant='h6'>
              Loans ({loans.length})
            </Typography>
            {loans.length > 0 ? (
              loans.map((loan) => <LoanItem key={loan.loanId} loan={loan} />)
            ) : (
              <Typography variant='body2'>No loans found</Typography>
            )}
          </div>
        </>
      ) : (
        <Typography variant='h4'>Customer not found</Typography>
      )}
    </>
  );
};

export default ProfilePage;
