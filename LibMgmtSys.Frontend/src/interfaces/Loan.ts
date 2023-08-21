interface Loan {
  loanId: string;
  bookId: string;
  bookTitle: string;
  customerId: string;
  customerEmail: string;
  loanedAt: string;
  dueDate: string;
  returnedAt: string | null;
}

export type { Loan };
