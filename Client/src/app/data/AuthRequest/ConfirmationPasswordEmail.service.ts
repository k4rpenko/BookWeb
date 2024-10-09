import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationEmail {
  http = inject(HttpClient)
  constructor() { }

  _ConfirmationEmail(email: String) {
    const json = {
      "email": email,
    };
    return this.http.post(`https://localhost:8081/api/AccountSettings/ConfirmationEmail`, json, {
      headers: { 'Content-Type': 'application/json' }
  });
  }
}
