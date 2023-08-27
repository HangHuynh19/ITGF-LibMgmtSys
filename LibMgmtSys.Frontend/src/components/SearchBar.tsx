import { Autocomplete, Box, TextField } from '@mui/material';
import useAppSelector from '../hooks/useAppSelector';
import { useState } from 'react';
import SearchIcon from '@mui/icons-material/Search';

interface SearchBarProps {
  onSearchTermSent: (searchTerm: string) => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ onSearchTermSent }) => {
  const books = useAppSelector((state) => state.bookReducer.books) || [];
  const [searchTerm, setSearchTerm] = useState('');

  const handleSearchTermChange = (
    _event: React.ChangeEvent<{}>,
    value: string
  ) => {
    setSearchTerm(value);
  };

  const onSearchIconClick = () => {
    onSearchTermSent(searchTerm);
  };

  return (
    <Box className='home-page__search-and-sort__search-bar'>
      <Autocomplete
        className='search-and-sort__search-bar__search-field'
        size='small'
        freeSolo
        disableClearable
        options={books.map((book) => book.title)}
        renderInput={(params) => (
          <TextField
            {...params}
            label='Search'
            margin='normal'
            variant='outlined'
            InputProps={{
              ...params.InputProps,
              type: 'search',
            }}
          />
        )}
        value={searchTerm}
        onInputChange={handleSearchTermChange}
      />
      <SearchIcon onClick={onSearchIconClick} />
    </Box>
  );
};

export default SearchBar;
