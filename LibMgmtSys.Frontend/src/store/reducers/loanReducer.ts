import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import {Loan} from "../../interfaces/Loan";
import {getCustomerLoans} from "../../api/apiCalls";
import axios from "axios";

const initialState: {
  loans: Loan[];
  loading: boolean;
  error: string | undefined | null;
} = {
  loans: [],
  loading: false,
  error: null,
}

export const fetchCustomerLoans = createAsyncThunk(
  'fetchCustomerLoans',
  async () => {
    try {
      const token = localStorage.getItem('token');

      if (!token) {
        return new Error('Token not found');
      }

      const response: Loan[] = await getCustomerLoans(token);
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

const loanSlice = createSlice({
  name: 'loan',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCustomerLoans.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(fetchCustomerLoans.fulfilled, (state, action) => {
        state.loans = action.payload as Loan[];
        state.loading = false;
      })
      .addCase(fetchCustomerLoans.rejected, (state, action) => {
        state.error = action.error.message;
        state.loading = false;
      });
  }
});

const loanReducer = loanSlice.reducer;

export default loanReducer;