import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  http = Inject(HttpClient)

  GetBookJson()
  {
    this.http.get('');
  }
}
