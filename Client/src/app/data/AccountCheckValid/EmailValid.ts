import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EmailValid {
  http = inject(HttpClient)
  constructor() { }

  PostValidToken(data: String) {
    const json = {
      jwt: data
    };
    return this.http.post(`https://localhost:8081/api/AccountSettings/CheckingPassword`, json, {
      headers: { 'Content-Type': 'application/json' }
  });
  }
}
