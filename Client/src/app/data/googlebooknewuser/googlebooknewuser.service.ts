import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { items } from '../interface/book.interface';

@Injectable({
  providedIn: 'root'
})
export class GooglebooknewuserService {
  http = inject(HttpClient)
  constructor() { }

  getBook() {
    return this.http.get<items>( `${window.location.origin}/api/Book/showbook_newuser?IdScroll=0`);
  }
}
