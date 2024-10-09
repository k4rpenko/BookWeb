import { Component, inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';
import { RegisterComponent } from '../../moduls/auth/register/register.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginService } from '../../data/AuthRequest/Login.service';
import { ConfirmationEmail } from '../../data/AuthRequest/ConfirmationPasswordEmail.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [  CommonModule, FormsModule],
  providers: [CookieService],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {
  constructor(public dialog: MatDialog, public dialogRef: MatDialogRef<AuthComponent>, private router: Router, private cookieService: CookieService) {}

  profileService = inject(LoginService);
  ConfirmationEmail = inject(ConfirmationEmail);

  onClose(): void {
    this.dialogRef.close();
  }

  RegisterForm(){
    this.dialog.open(RegisterComponent, {});
    this.dialogRef.close();
  }

  emailL: string = '';    
  passL: string = '';    
  emailError: string = '';
  passwordError: string = ''; 
  ConfirmationPassword: boolean = false;

  SendEmailConfirmation(){
    this.ConfirmationEmail._ConfirmationEmail(this.emailL).subscribe({});
  }

  onSubmit(): void {
    this.emailError = '';
    this.passwordError = '';

    if (!this.isValidEmail(this.emailL)) {
      this.emailError = 'Будь ласка, введіть коректну електронну пошту.';
    }

    if (!this.isValidPassword(this.passL)) {
      this.passwordError = 'Пароль має містити принаймні 6 символів, включаючи букви та цифри.';
    } 

    if (!this.emailError && !this.passwordError) {
      this.profileService.PostLogin(this.emailL, this.passL).subscribe({
        next: (response) => {
          const token = response.token;
          this.cookieService.set('authToken', token);
          //window.location.reload()
        },
        error: (error) => {
          console.log(error.status); 
          if (error.status === 401) {
            this.passwordError = 'Не вірний пароль, або електронна адреса';
          } else if (error.status === 400) {
            this.ConfirmationPassword = true;
            this.passwordError = 'Користувач існує, але не авторизований, підтвердіть електронну пошту';
          } else if (error.status === 429) {
            this.passwordError = 'Ви перевищили ліміт запитів';
          } else {
            console.log(error);
            
            this.passwordError = 'Сталася помилка, спробуйте ще раз';
          }
        }
      });
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
