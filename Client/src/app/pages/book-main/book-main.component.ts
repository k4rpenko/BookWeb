import { Component, inject } from '@angular/core';
import { BooksPageComponent } from "../../content/home/books/books-page/books-page.component";
import { GooglebooknewuserService } from "../../data/GET/googlebooknewuser/googlebooknewuser.service";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { CommonModule } from "@angular/common";
import { Book } from "../../data/interface/book.interface";
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-book-main',
  standalone: true,
  imports: [RouterOutlet, MatDialogModule, CommonModule, BooksPageComponent],
  templateUrl: './book-main.component.html',
  styleUrl: './book-main.component.scss'
})
export class BookMainComponent {

  profileService = inject(GooglebooknewuserService);
  book: Book[] = [];
  

  constructor() {this.profileService.getBook().subscribe(response  => {
    this.book = response.items;
  })}
}
