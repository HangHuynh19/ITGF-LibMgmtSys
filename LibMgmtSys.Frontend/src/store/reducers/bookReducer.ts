import { PayloadAction, createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { Book, UpsertBook } from '../../interfaces/Book';
import {
  createBook,
  deleteBook,
  getAllBooks,
  updateBook,
} from '../../api/apiCalls';
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
  async ({
    sortingOrder,
    searchTerm,
  }: {
    sortingOrder: string;
    searchTerm: string;
  }) => {
    try {
      const response: Book[] = await getAllBooks(sortingOrder, searchTerm);
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

export const postBook = createAsyncThunk(
  'postBook',
  async (bookToCreate: UpsertBook) => {
    try {
      var token = localStorage.getItem('token');

      if (!token) {
        return new Error('Token not found');
      }

      const response: Book = await createBook(token, bookToCreate);
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

export const putBook = createAsyncThunk(
  'putBook',
  async ({ id, bookToUpdate }: { id: string; bookToUpdate: UpsertBook }) => {
    try {
      var token = localStorage.getItem('token');

      if (!token) {
        return new Error('Token not found');
      }

      const response: Book = await updateBook(token, id, bookToUpdate);

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

export const removeBook = createAsyncThunk('removeBook', async (id: string) => {
  try {
    var token = localStorage.getItem('token');

    if (!token) {
      return new Error('Token not found');
    }

    const response: Book = await deleteBook(token, id);

    return response;
  } catch (err) {
    if (axios.isAxiosError(err)) {
      return err.response?.data;
    } else {
      return err;
    }
  }
});

const bookSlice = createSlice({
  name: 'book',
  initialState,
  reducers: {
    filterBooks: (state, action: PayloadAction<{ ids: string[] }>) => {
      const { ids } = action.payload;
      state.books = state.books.filter((book) => ids.includes(book.id));
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllBooks.pending, (state) => {
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
      })
      .addCase(postBook.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(postBook.fulfilled, (state, action) => {
        state.loading = false;
        state.books = [...state.books, action.payload];
        state.error = null;
      })
      .addCase(postBook.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      })
      .addCase(putBook.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(putBook.fulfilled, (state, action) => {
        state.loading = false;
        state.books = state.books.map((book) =>
          book.id === action.payload.id ? action.payload : book
        );
        state.error = null;
      })
      .addCase(putBook.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      })
      .addCase(removeBook.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(removeBook.fulfilled, (state, action) => {
        state.loading = false;
        state.books = state.books.filter(
          (book) => book.id !== action.payload.id
        );
        state.error = null;
      })
      .addCase(removeBook.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      });
  },
});

const productReducer = bookSlice.reducer;
export const { filterBooks } = bookSlice.actions;
export default productReducer;
