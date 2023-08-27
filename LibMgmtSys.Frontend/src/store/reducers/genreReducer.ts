import axios from 'axios';
import { getAllGenres } from '../../api/apiCalls';
import { Genre } from '../../interfaces/Genre';
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';

const initialState: {
  genres: Genre[];
  loading: boolean;
  error: string | undefined | null;
} = {
  genres: [],
  loading: false,
  error: null,
};

export const fetchAllGenres = createAsyncThunk('fetchAllGenres', async () => {
  try {
    const token = localStorage.getItem('token');

    if (!token) {
      return new Error('Token not found');
    }

    const response: Genre[] = await getAllGenres(token);
    return response;
  } catch (err) {
    if (axios.isAxiosError(err)) {
      return err.response?.data;
    } else {
      return err;
    }
  }
});

const genreSlice = createSlice({
  name: 'genre',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllGenres.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchAllGenres.fulfilled, (state, action) => {
        state.genres = action.payload as Genre[];
        state.loading = false;
      })
      .addCase(fetchAllGenres.rejected, (state, action) => {
        state.error = action.error.message;
        state.loading = false;
      });
  },
});

const genreReducer = genreSlice.reducer;

export default genreReducer;
