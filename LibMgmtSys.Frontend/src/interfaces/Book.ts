interface Book {
  id: string;
  title: string;
  authorNames: string[];
  isbn: string;
  publisher: string;
  year: number;
  genreNames: string[];
  description: string;
  borrowingPeriod: number;
  quantity: number;
}

export type { Book };
