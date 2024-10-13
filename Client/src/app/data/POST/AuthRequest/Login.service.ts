import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  http = inject(HttpClient)
  constructor() { }

  PostLogin(email: String, password: String) {
    const json = {
      "email": email,
      "password": password
    };
    return this.http.post<{ token	: string }>(`${window.location.origin}/api/Auth/login`, json, {
      headers: { 'Content-Type': 'application/json' }
  });
  }
}
