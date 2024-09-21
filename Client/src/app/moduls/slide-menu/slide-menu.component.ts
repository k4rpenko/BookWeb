import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-slide-menu',
  standalone: true,
  imports: [],
  templateUrl: './slide-menu.component.html',
  styleUrl: './slide-menu.component.scss'
})
export class SlideMenuComponent {
  constructor(public dialogRef: MatDialogRef<SlideMenuComponent>) {}

  onClose(): void {
    this.dialogRef.close();
  }
}
