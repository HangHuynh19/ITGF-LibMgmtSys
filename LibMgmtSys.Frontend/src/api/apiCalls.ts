import axios from "axios";

const url = "http://localhost:5271/api/v1";

const getAllBooks = async <T>(sortingOrder: string): Promise<T> => {
  const response = await axios.get(`${url}/books?sortOrder=${sortingOrder}`);
  const data = await response.data;
  return data;
}

export {getAllBooks}