import { Component, inject, OnInit, Type } from "@angular/core";
import { GooglebooknewuserService } from "./data/googlebooknewuser/googlebooknewuser.service";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { HeaderComponent } from "./content/header/header.component";
import { CommonModule } from "@angular/common";
import { BooksPageComponent } from "./content/home/books/books-page/books-page.component";
import { Book } from "./data/interface/book.interface";
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { updateAccetsToken } from "./data/updateJWT/updateAccetsToken.service";
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, MatDialogModule, CommonModule, BooksPageComponent],
  providers: [CookieService],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'angular';
  private lastRequestTime: Date | null = null;
  profileService = inject(updateAccetsToken);
  private readonly REQUEST_INTERVAL = 1500000;
  token: string = '';

  constructor(private route: ActivatedRoute, private router: Router, private cookieService: CookieService) {}


  ngOnInit(): void {
    const now = new Date();

    if (!this.lastRequestTime || now.getTime() - this.lastRequestTime.getTime() > this.REQUEST_INTERVAL) {
      this.token = this.cookieService.get('authToken');
      this.lastRequestTime = now;
      if( this.token !== '' || this.token !== null){
        this.profileService.updateAccetsToken(this.token).subscribe({
          next: (response) => {
            const token = response.token;
            this.cookieService.set('authToken', token);
            this.router.navigate(['/']);
          },
          error: (error) => {
            console.error('Помилка при виконанні POST запиту:', error);
          }
        });
      }
    }
  }
}