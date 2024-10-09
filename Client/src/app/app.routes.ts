import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { AppComponent } from './app.component';
import { BookMainComponent } from './pages/book-main/book-main.component';
import { AccountTypeComponent } from './pages/account-type/account-type.component';

export const routes: Routes = [
    { path: 'reset-password/:token/:type', component: AccountTypeComponent },
    { path: '', component: BookMainComponent, pathMatch: 'full' },
    { path: '**', component: PageNotFoundComponent }
];
  