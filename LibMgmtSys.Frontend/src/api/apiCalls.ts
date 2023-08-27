import axios from 'axios';
import { User } from '../interfaces/User';
import { UpsertBook } from '../interfaces/Book';

const url = 'http://3.249.217.176/api/v1';

const getAllBooks = async <T>(
  sortingOrder: string,
  searchTerm: string
): Promise<T> => {
  if (searchTerm) {
    const response = await axios.get(
      `${url}/books?pageNumber=1&pageSize=100&sortOrder=${sortingOrder}&searchTerm=${searchTerm}`
    );
    const data = await response.data;

    return data;
  }

  const response = await axios.get(
    `${url}/books?pageNumber=1&pageSize=100&sortOrder=${sortingOrder}`
  );
  const data = await response.data;

  return data;
};

const loggingIn = async <T>(email: string, password: string): Promise<T> => {
  const response = await axios.post(`${url}/auth/login`, {
    email,
    password,
  });
  const data = await response.data;
  return data;
};

const register = async <T>(user: User): Promise<T> => {
  const response = await axios.post(`${url}/auth/register`, user);
  const data = await response.data;
  return data;
};

const updateUser = async <T>(user: User, token: string): Promise<T> => {
  const headers = {
    Authorization: `Bearer ${token}`,
  };
  const response = await axios.put(`${url}/users/edit`, user, { headers });
  const data = await response.data;
  return data;
};

const getCustomerProfile = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/customers/profile`, {
    headers: { Authorization: `Bearer ${token}` },
    validateStatus: (status) => status >= 200 && status < 300,
  });

  if (response.status !== 200) {
    throw new Error('Customer profile not found');
  }

  const data = await response.data;
  return data;
};

const getCustomerLoans = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/loans/own-loans`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  return data;
};

const createLoansFromBookIds = async <T>(
  ids: string[],
  token: string
): Promise<T> => {
  try {
    const response = await axios.post(
      `${url}/loans`,
      {
        bookIds: ids,
        loanedAt: new Date().toISOString(),
      },
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    const data = await response.data;
    return data;
  } catch (err) {
    if (axios.isAxiosError(err)) {
      return err.response?.data;
    } else {
      return err as T;
    }
  }
};

const checkAdminStatus = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/users/check-admin-status`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  return data;
};

const getAllGenres = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/genres`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  return data;
};

const getAllAuthors = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/authors`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  return data;
};

const createBook = async <T>(
  token: string,
  bookToCreate: UpsertBook
): Promise<T> => {
  const response = await axios.post(`${url}/books`, bookToCreate, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  return data;
};

const updateBook = async <T>(
  token: string,
  id: string,
  bookToUpdate: UpsertBook
): Promise<T> => {
  const response = await axios.put(`${url}/books/${id}`, bookToUpdate, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  return data;
};

const deleteBook = async <T>(token: string, id: string): Promise<T> => {
  const response = await axios.delete(`${url}/books/${id}`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  return data;
};

export {
  getAllBooks,
  loggingIn,
  register,
  updateUser,
  getCustomerProfile,
  getCustomerLoans,
  createLoansFromBookIds,
  checkAdminStatus,
  getAllGenres,
  getAllAuthors,
  createBook,
  updateBook,
  deleteBook,
};
