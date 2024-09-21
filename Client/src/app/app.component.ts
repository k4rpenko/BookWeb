import { Component, inject, Type } from "@angular/core";
import { GooglebooknewuserService } from "./data/googlebooknewuser/googlebooknewuser.service";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { HeaderComponent } from "./content/header/header.component";
import { CommonModule } from "@angular/common";
import { BooksPageComponent } from "./content/home/books/books-page/books-page.component";
import { Book } from "./data/interface/book.interface";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, MatDialogModule, CommonModule, BooksPageComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'angular';

  profileService = inject(GooglebooknewuserService);
  book: Book[] = [];
  

  constructor() {this.profileService.getBook().subscribe(response  => {
    this.book = response.items;
    console.log(response);
  })}
}
