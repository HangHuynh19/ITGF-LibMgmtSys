import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { Author } from '../../interfaces/Author';
import axios from 'axios';
import { getAllAuthors } from '../../api/apiCalls';

const initialState: {
  authors: Author[];
  loading: boolean;
  error: string | undefined | null;
} = {
  authors: [],
  loading: false,
  error: null,
};

export const fetchAllAuthors = createAsyncThunk('fetchAllAuthors', async () => {
  try {
    const token = localStorage.getItem('token');

    if (!token) {
      return new Error('Token not found');
    }

    const response: Author[] = await getAllAuthors(token);

    return response;
  } catch (err) {
    if (axios.isAxiosError(err)) {
      return err.response?.data;
    } else {
      return err;
    }
  }
});

const authorSlice = createSlice({
  name: 'author',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllAuthors.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(fetchAllAuthors.fulfilled, (state, action) => {
        state.authors = action.payload as Author[];
        state.loading = false;
      })
      .addCase(fetchAllAuthors.rejected, (state, action) => {
        state.error = action.error.message;
        state.loading = false;
      });
  },
});

const authorReducer = authorSlice.reducer;

export default authorReducer;