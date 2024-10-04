import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';
import { AuthComponent } from '../../../moduls/auth/auth.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [  CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  constructor(public dialog: MatDialog, public dialogRef: MatDialogRef<RegisterComponent>) {}

  onClose(): void {
    this.dialogRef.close();
  }

  LoginForm(){
    this.dialog.open(AuthComponent, {});
    this.dialogRef.close();
  }

  emailR: string = '';    
  pass1R: string = '';   
  pass2R: string = '';    
  emailError: string = '';
  passwordError: string = ''; 

  onSubmit(): void {
    this.emailError = '';
    this.passwordError = '';

    if (!this.isValidEmail(this.emailR)) {
      this.emailError = 'Будь ласка, введіть коректну електронну пошту.';
    }

    if (!this.isValidPassword(this.pass1R)) {
      this.passwordError = 'Пароль має містити принаймні 6 символів, включаючи букви та цифри.';
    } else if (this.pass1R !== this.pass2R) {
      this.passwordError = 'Паролі не співпадають.';
    }

    if (!this.emailError && !this.passwordError) {
      const formData = {
        emailF: this.emailR,
        pass1F: this.pass1R,
        pass2F: this.pass2R,
      };
      console.log('Дані форми:', formData);
    }
  }

  isValidEmail(email: string): boolean {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
  }

  isValidPassword(password: string): boolean {
    const hasUpperCase = /[A-Z]/.test(password); 
    const hasLowerCase = /[a-z]/.test(password); 
    const hasNumber = /\d/.test(password); 
    const isLongEnough = password.length >= 6; 
  
    return hasUpperCase && hasLowerCase && hasNumber && isLongEnough; 
  }
}
