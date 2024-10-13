import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AccountContent } from '../../interface/Account.interface';

@Injectable({
  providedIn: 'root'
})
export class AccountContents {
  http = inject(HttpClient)
  AccountContents() {}

  GetAccountContent(id: string) {
    return this.http.get<AccountContent>(`${window.location.origin}/api/Account/${id}`);
  }
}
