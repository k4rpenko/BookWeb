import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SlideMenuComponent } from '../../moduls/slide-menu/slide-menu.component';
import { AuthComponent } from '../../moduls/auth/auth.component';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(public dialog: MatDialog) {}

  openDialog(): void {
    this.dialog.open(SlideMenuComponent, {
      width: '300px',
      height: '100%',
      position: { left: '0', top: '0' },
      panelClass: 'custom-dialog-container',
    });
  }

  openAuth(): void {
    this.dialog.open(AuthComponent, {});
  }
}
