export interface Content {
  id: string;
  firstName: string;
  lastName: string | null; 
  avatar: string; 
  books: string[]; 
  userName: string; 
  email: string; 
  concurrencyStamp: string; 
  phoneNumber: string | null; 
}

export interface AccountContent {
  account: Content;
}
