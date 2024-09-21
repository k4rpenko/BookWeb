import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
    { path: '',  pathMatch: 'full' },
    { path: '**',  component: PageNotFoundComponent }
];
