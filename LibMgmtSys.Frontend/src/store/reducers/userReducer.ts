import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { User } from '../../interfaces/User';
import { checkAdminStatus, register, updateUser } from '../../api/apiCalls';
import axios from 'axios';

const initialState: {
  user: User | null;
  isAdmin: boolean;
  loading: boolean;
  error: string | null | undefined;
} = {
  user: null,
  isAdmin: false,
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

export const checkIsAmin = createAsyncThunk('checkIsAmin', async () => {
  try {
    var token = localStorage.getItem('token');
    if (token) {
      const response: boolean = await checkAdminStatus(token);
      return response;
    }
  } catch (err) {
    if (axios.isAxiosError(err)) {
      return err.response?.data;
    } else {
      return err;
    }
  }
});

export const putUser = createAsyncThunk('putUser', async (user: User) => {
  try {
    var token = localStorage.getItem('token');
    if (!token) {
      return new Error('No token found');
    }
    const response: User = await updateUser(user, token);
    console.log('putUser', response);
    return response;
  } catch (err) {
    if (axios.isAxiosError(err)) {
      return err.response?.data;
    } else {
      return err;
    }
  }
});

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
      .addCase(registerUser.pending, (state) => {
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
      })
      .addCase(checkIsAmin.pending, (state) => {
        state.loading = true;
      })
      .addCase(checkIsAmin.fulfilled, (state, action) => {
        state.loading = false;
        state.isAdmin = action.payload;
        state.error = null;
      })
      .addCase(checkIsAmin.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      })
      .addCase(putUser.pending, (state) => {
        state.loading = true;
      })
      .addCase(putUser.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload;
        state.error = null;
      })
      .addCase(putUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      });
  },
});

const userReducer = userSlice.reducer;

export const { clearUser } = userSlice.actions;
export default userReducer;
