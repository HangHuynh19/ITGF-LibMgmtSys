import axios from 'axios';
import { User } from '../interfaces/User';
import { v4 as uuidv4 } from 'uuid';

const url = 'http://localhost:5271/api/v1';

const getAllBooks = async <T>(sortingOrder: string): Promise<T> => {
  const response = await axios.get(
    `${url}/books?pageNumber=1&pageSize=100&sortOrder=asc`
  );
  const data = await response.data;

  return data;
};

/* const getBookById = async <T>(id: string): Promise<T> => {
  const response = await axios.get(`${url}/books/${id}`);
  const data = await response.data;

  return data;
}; */

const loggingIn = async <T>(email: string, password: string): Promise<T> => {
  const response = await axios.post(`${url}/auth/login`, {
    email,
    password,
  });
  const data = await response.data;
  //console.log('loggingIn', data);
  return data;
};

const register = async <T>(user: User): Promise<T> => {
  const response = await axios.post(`${url}/auth/register`, user);
  const data = await response.data;
  //console.log('register', data);
  return data;
};

/* const putUser = async <T>(user: User, token: string): Promise<T> => {
  const headers = {
    Authorization: `Bearer ${token}`,
  };
  const response = await axios.put(`${url}/users/:id`, user, { headers });
  const data = await response.data;
  console.log('putUser', data);
  return data;
}; */

const getCustomerProfile = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/customers/profile`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  //console.log('getCustomerProfile', data);
  return data;
};

const getBooksByIds = async <T>(ids: string[]): Promise<T> => {
  const response = await axios.get(`${url}/books/by-id`, { params: { ids } });
  const data = await response.data;
  //console.log('getBooksByIds', data);
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
    //console.log('createLoansFromBookIds', data);
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
  console.log('checkAdminStatus', data);
  return data;
};

const getAllGenres = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/genres`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  console.log('getAllGenres', data);
  return data;
};

const getAllAuthors = async <T>(token: string): Promise<T> => {
  const response = await axios.get(`${url}/authors`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  const data = await response.data;
  console.log('getAllAuthors', data);
  return data;
};

export {
  getAllBooks,
  /* getBookById, */ loggingIn,
  register,
  /* putUser */ getCustomerProfile,
  getBooksByIds,
  createLoansFromBookIds,
  checkAdminStatus,
  getAllGenres,
  getAllAuthors,
};
