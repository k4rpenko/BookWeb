import { Component, inject, Input, input } from '@angular/core';
import { Book } from "../../../../data/interface/book.interface";

@Component({
  selector: 'app-books-page',
  standalone: true,
  imports: [],
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.scss'
})
export class BooksPageComponent {
  @Input() books?: Book;
}
