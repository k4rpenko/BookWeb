import { Component, inject, Input, input } from '@angular/core';
import { Book } from "../../../../data/interface/book.interface";
import { CartService } from '../../../../data/card/cart.service';

@Component({
  selector: 'app-books-page',
  standalone: true,
  imports: [],
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.scss'
})
export class BooksPageComponent {
  @Input() books?: Book;

  constructor(private cartService: CartService) {
  }

  AddToCard(book: Book | undefined):void {
    const item = {
      id: book?.id.toString(),
      title: book?.volumeInfo.title.toString(),
      price: Number(book?.saleInfo.listPrice?.amount) || 0,
      image: book?.volumeInfo.imageLinks.smallThumbnail,
      quantity: 1
    }
    this.cartService.AddToCard(item);
  }
}
