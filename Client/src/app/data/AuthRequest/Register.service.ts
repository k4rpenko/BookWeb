import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  http = inject(HttpClient)
  constructor() { }

  PostRegister(email: String, password: String) {
    const json = {
      "email": email,
      "password": password
    };
    return this.http.post<{ token	: string }>(`https://localhost:8081/api/Auth/registration`, json, {
      headers: { 'Content-Type': 'application/json' }
  });
  }
}
