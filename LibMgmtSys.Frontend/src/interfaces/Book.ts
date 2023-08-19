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

interface UpsertBook {
  title: string;
  isbn: string;
  publisher: string;
  authorIds?: string[];
  year: number;
  description: string;
  genreIds?: string[];
  image?: string;
  borrowingPeriod: number;
  quantity: number;
}

export type { Book, UpsertBook };
