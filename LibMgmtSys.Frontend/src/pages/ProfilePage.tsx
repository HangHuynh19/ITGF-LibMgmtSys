import {Button, Typography} from "@mui/material";
import useAppSelector from "../hooks/useAppSelector";
import MailOutlineIcon from '@mui/icons-material/MailOutline';

const ProfilePage = () => {
  const customer = useAppSelector((state) => state.customerReducer.customer);
  const randomInt = Math.floor(Math.random() * 8) + 1;

  return (
    <>
      {customer ? (
        <div>
          <div>
            <img
              src={require(`../assets/profileImages/cat${randomInt}.jpeg?w=164&h=164&fit=crop&auto=format`)}
              alt="profile"
              loading='lazy'
            />
          </div>
          <article>
            <Typography variant="h4">
              {customer.firstName} {customer.lastName}
            </Typography>
            <Typography component={'p'}>
              <MailOutlineIcon fontSize='small'/> {customer.email}
            </Typography>
            <Button variant="contained" color="primary">
              Edit Profile            
            </Button>
          </article>
        </div>
      ) : (
        <Typography variant="h4">Customer not found</Typography>
      )}
    </>
  );
};

export default ProfilePage;
