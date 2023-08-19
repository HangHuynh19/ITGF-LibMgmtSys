import { configureStore } from '@reduxjs/toolkit';
import bookReducer from './reducers/bookReducer';
import authReducer from './reducers/authReducer';
import userReducer from './reducers/userReducer';
import customerReducer from './reducers/customerReducer';
import cartReducer from './reducers/cartReducer';
import authorReducer from './reducers/authorReducer';
import genreReducer from './reducers/genreReducer';

const cartData = JSON.parse(localStorage.getItem('cart') || '[]');

const store = configureStore({
  reducer: {
    bookReducer: bookReducer,
    authReducer: authReducer,
    userReducer: userReducer,
    customerReducer: customerReducer,
    cartReducer: cartReducer,
    authorReducer: authorReducer,
    genreReducer: genreReducer,
  },
  preloadedState: {
    cartReducer: cartData,
  },
});

store.subscribe(() => {
  localStorage.setItem('cart', JSON.stringify(store.getState().cartReducer));
});

export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
