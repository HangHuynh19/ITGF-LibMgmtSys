import { PayloadAction, createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { Book } from '../../interfaces/Book';
import axios from 'axios';
import { getBooksByIds } from '../../api/apiCalls';

const initialState: {
  cart: string[];
  /* booksInCart: Book[]; */
  totalQuantity: number;
  loading: boolean;
  error: string | null | undefined;
} = {
  cart: [],
  /* booksInCart: [], */
  totalQuantity: 0,
  loading: false,
  error: null,
};

/* export const fetchBooksInCart = createAsyncThunk(
  'fetchBooksInCart',
  async (bookIds: string[]) => {
    try {
      const response = await getBooksByIds(bookIds);
      return response;
    } catch (err) {
      if (axios.isAxiosError(err)) {
        return err.response?.data;
      } else {
        return err;
      }
    }
  }
); */

const cartSlice = createSlice({
  name: 'cart',
  initialState: initialState,
  reducers: {
    addToCart: (
      state: { cart: string[]; totalQuantity: number },
      action: PayloadAction<string>
    ) => {
      const bookId = action.payload;
      const bookIndex = state.cart.findIndex((id) => id === bookId);
      if (bookIndex === -1) {
        state.cart.push(bookId);
      }
      state.totalQuantity = state.cart.length;
      localStorage.setItem('cart', JSON.stringify(state.cart));
    },
    removeFromCart: (
      state: { cart: string[]; totalQuantity: number },
      action: PayloadAction<string>
    ) => {
      const bookId = action.payload;
      const bookIndex = state.cart.findIndex((id) => id === bookId);
      if (bookIndex !== -1) {
        state.cart.splice(bookIndex, 1);
      }
      state.totalQuantity = state.cart.length;
      localStorage.setItem('cart', JSON.stringify(state.cart));
    },
    clearCart: (state: { cart: string[]; totalQuantity: number }) => {
      return initialState;
    },
  },
  /* extraReducers: (builder) => {
    builder
      .addCase(fetchBooksInCart.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchBooksInCart.fulfilled, (state, action) => {
        state.booksInCart = action.payload;
        state.loading = false;
        state.error = null;
      })
      .addCase(fetchBooksInCart.rejected, (state, action) => {
        state.error = action.error.message;
        state.loading = false;
      });
  }, */
});

const cartReducer = cartSlice.reducer;

export const { addToCart, removeFromCart, clearCart } = cartSlice.actions;
export default cartReducer;
