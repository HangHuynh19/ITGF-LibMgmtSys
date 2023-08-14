import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { Book } from '../../interfaces/Book';
import { getAllBooks, getBookById } from '../../api/apiCalls';
import axios from 'axios';

const initialState: {
  books: Book[];
  loading: boolean;
  error: string | undefined | null;
} = {
  books: [],
  loading: false,
  error: null,
};

export const fetchAllBooks = createAsyncThunk(
  'fetchAllBooks',
  async (sortingOrder: string) => {
    try {
      const response: Book[] = await getAllBooks(sortingOrder);
      return response;
    } catch (err) {
      if (axios.isAxiosError(err)) {
        return err.response?.data;
      } else {
        return err;
      }
    }
  }
);

/* export const fetchBookById = createAsyncThunk(
  'fetchBookById',
  async (id: string) => {
    try {
      const response: Book = await getBookById(id);
      console.log('fetchBookById reducer', response);
      return response;
    } catch (err) {
      if (axios.isAxiosError(err)) {
        return err.response?.data;
      } else {
        return err;
      }
    }
  }); */

const bookSlice = createSlice({
  name: 'book',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllBooks.pending, (state, action) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAllBooks.fulfilled, (state, action) => {
        state.loading = false;
        state.books = action.payload;
        console.log('reducer slices', state.books);
        state.error = null;
      })
      .addCase(fetchAllBooks.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      });
    /* .addCase(fetchAllBooks.pending, (state, action) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchBookById.fulfilled, (state, action) => {
        state.loading = false;
        state.books = [action.payload];
        state.error = null;
      })
      .addCase(fetchBookById.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      }); */
  },
});

const productReducer = bookSlice.reducer;

export default productReducer;
