import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { loggingIn } from '../../api/apiCalls';
import axios from 'axios';

const token = localStorage.getItem('token');
const initialState: {
  isLoggedIn: boolean;
  isLoading: boolean;
  error: string | null | undefined;
} = {
  isLoggedIn: token ? true : false,
  isLoading: false,
  error: null,
};

export const authenticate = createAsyncThunk(
  'authenticate',
  async ({ email, password }: { email: string; password: string }) => {
    try {
      const response: { token: string } = await loggingIn(email, password);

      localStorage.setItem('token', response.token);
      console.log('authReducer', response);
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

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      localStorage.removeItem('token');
      state.isLoggedIn = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(authenticate.pending, (state, action) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(authenticate.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isLoggedIn = true;
        state.error = null;
      })
      .addCase(authenticate.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message;
      });
  },
});

const authReducer = authSlice.reducer;

export const { logout } = authSlice.actions;
export default authReducer;
