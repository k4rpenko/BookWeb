import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {
  constructor(public dialogRef: MatDialogRef<AuthComponent>) {}

  onClose(): void {
    this.dialogRef.close();
  }
}
