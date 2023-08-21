import {Autocomplete, Box, TextField} from "@mui/material";
import useAppSelector from "../hooks/useAppSelector";
import {useState} from "react";
import SearchIcon from '@mui/icons-material/Search';

const SearchBar = () => {
  const books = useAppSelector((state) => state.bookReducer.books);
  const [searchTerm, setSearchTerm] = useState('');

  const handleSearchTermChange = (
    event: React.ChangeEvent<{}>,
    value: string
  ) => {
    setSearchTerm(value);
  };

  return (
    <Box>
      <Autocomplete 
        size="small"
        freeSolo
        options={books.map((book) => book.title)}
        renderInput={(params) => (
          <TextField
            {...params}
            label="Search"
            margin="normal"
            variant="outlined"
            InputProps={{
              ...params.InputProps,
              type: 'search',
            }}
          />
        )}
        value={searchTerm}
        onInputChange={handleSearchTermChange}
      />
      <SearchIcon />
    </Box>
  );
}

export default SearchBar;