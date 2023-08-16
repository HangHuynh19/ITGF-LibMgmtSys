import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { User } from '../../interfaces/User';
import { register } from '../../api/apiCalls';
import axios from 'axios';

const initialState: {
  user: User | null;
  loading: boolean;
  error: string | null | undefined;
} = {
  user: null,
  loading: false,
  error: null,
};

export const registerUser = createAsyncThunk(
  'registerUser',
  async (user: User) => {
    try {
      const response: { token: string } = await register(user);
      localStorage.setItem('token', response.token);
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

const userSlice = createSlice({
  name: 'user',
  initialState: initialState,
  reducers: {
    clearUser: () => {
      return initialState;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(registerUser.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(registerUser.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload;
        state.error = null;
      })
      .addCase(registerUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      });
  },
});

const userReducer = userSlice.reducer;

export const { clearUser } = userSlice.actions;
export default userReducer;
