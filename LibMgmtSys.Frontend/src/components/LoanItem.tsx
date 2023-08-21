import { Card, CardContent, CardMedia, Typography } from '@mui/material';
import { Loan } from '../interfaces/Loan';

const LoanItem = ({ loan }: { loan: Loan }) => {
  const randomInt = Math.floor(Math.random() * 15) + 1;

  return (
    <Card className='loans__loan-item'>
      <CardMedia
        className='loans__loan-item__img'
        component='img'
        image={require(`../assets/bookImages/book${randomInt}.jpeg?w=164&h=164&fit=crop&auto=format`)}
        alt={loan.bookTitle}
      />
      <CardContent>
        <Typography variant='subtitle1'>
          <b>Book: </b>
          {loan.bookTitle}
        </Typography>
        <Typography variant='body2'>
          <b>Loan At: </b>
          {loan.loanedAt}
        </Typography>
        <Typography variant='body2'>
          <b>Due Date: </b>
          {loan.dueDate}
        </Typography>
        {loan.returnedAt !== null && (
          <Typography variant='body2'>
            <b>Returned At: </b>
            {loan.returnedAt}
          </Typography>
        )}
      </CardContent>
    </Card>
  );
};

export default LoanItem;
