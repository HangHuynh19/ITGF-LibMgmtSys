import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { getCustomerProfile } from '../../api/apiCalls';
import { Customer } from '../../interfaces/Customer';
import axios from 'axios';

const initialState: {
  customer: Customer | null;
  loading: boolean;
  error: string | null | undefined;
} = {
  customer: null,
  loading: false,
  error: null,
};

export const fetchCustomerProfile = createAsyncThunk(
  'fetchCustomerProfile',
  async () => {
    try {
      const token = localStorage.getItem('token');

      if (!token) {
        return Error('No token found');
      }
      console.log('fetchCustomerProfile', token);
      const response: Customer = await getCustomerProfile(token);

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

const customerSlice = createSlice({
  name: 'customer',
  initialState: initialState,
  reducers: {
    clearCustomer: () => {
      return initialState;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCustomerProfile.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(fetchCustomerProfile.fulfilled, (state, action) => {
        state.loading = false;
        state.customer = action.payload;
        state.error = null;
      })
      .addCase(fetchCustomerProfile.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      });
  },
});

const customerReducer = customerSlice.reducer;

export const { clearCustomer } = customerSlice.actions;
export default customerReducer;
