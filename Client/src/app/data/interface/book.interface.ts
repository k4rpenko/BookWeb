export interface VolumeInfo {
  title: string;
  authors: string[] | null;
  pageCount: number;
  imageLinks: ImageLinks;
  publishedDate: string | null;
}

export interface ImageLinks {
  smallThumbnail: string;
  thumbnail: string;
}

export interface listPrice {
  amount: any,
  currencyCode: string;
}

export interface SaleInfo {
  country: string;
  saleability: string;
  listPrice: listPrice
}

export interface Book {
  id: string;
  volumeInfo: VolumeInfo;
  saleInfo: SaleInfo;
}

export interface items {
  items: Array<Book>;
}
