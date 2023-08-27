import { PayloadAction, createSlice } from '@reduxjs/toolkit';

const initialState: {
  cart: string[];
  totalQuantity: number;
  loading: boolean;
  error: string | null | undefined;
} = {
  cart: [],
  totalQuantity: 0,
  loading: false,
  error: null,
};

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
    clearCart: () => {
      return initialState;
    },
  },
});

const cartReducer = cartSlice.reducer;

export const { addToCart, removeFromCart, clearCart } = cartSlice.actions;
export default cartReducer;
