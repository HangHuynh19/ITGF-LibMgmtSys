import {configureStore} from "@reduxjs/toolkit";
import bookReducer from "./reducers/bookReducer";
import authReducer from "./reducers/authReducer";
import userReducer from "./reducers/userReducer";
import customerReducer from "./reducers/customerReducer";

const store = configureStore({
  reducer: {
    bookReducer: bookReducer,
    authReducer: authReducer,
    userReducer: userReducer,
    customerReducer: customerReducer,
  }
});

export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;