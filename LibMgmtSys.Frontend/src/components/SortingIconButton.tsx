import { useState } from 'react';
import {
  Box,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
  Typography,
} from '@mui/material';

interface SortingIconButtonProps {
  onSortingOrderSent: (sortingOrder: string) => void;
}

const SortingIconButton: React.FC<SortingIconButtonProps> = ({
  onSortingOrderSent,
}) => {
  const [sortingOrder, setSortingOrder] = useState('asc');

  const handleSortingOrderChange = (event: SelectChangeEvent<string>) => {
    setSortingOrder(event.target.value);
    onSortingOrderSent(event.target.value);
  };

  return (
    <Box>
      <FormControl color='secondary' size='small'>
        <InputLabel
          htmlFor='sort-by-title'
          variant='outlined'
          size='normal'
          shrink={true}
        >
          <b>Sort by &nbsp;</b>
        </InputLabel>
        <Select
          labelId='sort-by'
          id='sort-by'
          label='Sort by'
          value={sortingOrder}
          defaultValue='asc'
          onChange={handleSortingOrderChange}
          sx={{ marginTop: '0.45rem', fontSize: '0.75em' }}
        >
          <MenuItem value='asc' dense={true}>
            <Typography component='p'>Title | A to Z</Typography>
          </MenuItem>
          <MenuItem value='desc' dense={true}>
            <Typography component='p'>Title | Z to A</Typography>
          </MenuItem>
        </Select>
      </FormControl>
    </Box>
  );
};

export default SortingIconButton;
