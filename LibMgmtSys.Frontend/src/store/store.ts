import {configureStore} from "@reduxjs/toolkit";
import bookReducer from "./reducers/bookReducer";

const store = configureStore({
  reducer: {
    bookReducer: bookReducer
  }
});

export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;