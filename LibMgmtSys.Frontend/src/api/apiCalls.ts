import axios from 'axios';

const url = 'http://localhost:5271/api/v1';

const getAllBooks = async <T>(sortingOrder: string): Promise<T> => {
  const response = await axios.get(
    `${url}/books?pageNumber=1&pageSize=100&sortOrder=asc`
  );
  const data = await response.data;

  return data;
};

const getBookById = async <T>(id: string): Promise<T> => {
  const response = await axios.get(`${url}/books/${id}`);
  const data = await response.data;
  
  return data;
};

const loggingIn = async <T>(email: string, password: string): Promise<T> => {
  const response = await axios.post(`${url}/auth/login`, {
    email,
    password,
  });
  const data = await response.data;
  console.log('loggingIn', data);
  return data;
};

export { getAllBooks, getBookById, loggingIn };
